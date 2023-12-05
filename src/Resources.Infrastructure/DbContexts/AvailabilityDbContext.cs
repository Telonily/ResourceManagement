using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Resources.Application.Exceptions;
using Resources.Core.Entities;
using Resources.Core.Repositories;
using Resources.Core.ValueObjects;

namespace Resources.Endpoint.Availabaility.Domain.DbContexts;

public class AvailabilityDbContextOptions
{
    public required string ConnectionString { get; set; }
    public int TemporaryBlockadeInMinutes { get; set; } = 60;
}


public class AvailabilityDbContext : DbContext, IResourceBlockadesRepository
{
    private readonly AvailabilityDbContextOptions Options;

    public AvailabilityDbContext(IOptions<AvailabilityDbContextOptions> options)
    {
        Options = options.Value;
    }

    public DbSet<ResourceBlockade> ResourceBlockades { get; set; }
    IEnumerable<ResourceBlockade> IResourceBlockadesRepository.ResourceBlockades { get => ResourceBlockades; set { } }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Options.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Availability");

        modelBuilder.Entity<ResourceBlockade>().HasIndex(x => x.ResourceId);

        base.OnModelCreating(modelBuilder);
    }

    public void CreateBlockade(ResourceId resourceId, Guid ownerId)
    {
        CheckIfActiveBlockade(resourceId);

        ResourceBlockade newBlockade = new
        (
            Guid.NewGuid(),
            resourceId,
            ownerId,
            DateTime.Now,
            TimeSpan.FromMinutes(Options.TemporaryBlockadeInMinutes)
        );

        ResourceBlockades.Add(newBlockade);
        SaveChanges();
    }

    public void CreatePermanentBlockade(ResourceId resourceId, Guid ownerId)
    {
        CheckIfActiveBlockade(resourceId);

        ResourceBlockade newBlockade = new(Guid.NewGuid(), resourceId, ownerId, DateTime.Now, TimeSpan.Zero);

        ResourceBlockades.Add(newBlockade);
        SaveChanges();
    }

    public void ReleaseBlockade(ResourceId resourceId, Guid ownerId)
    {
        var blockade = ResourceBlockades.Where(b => b.ResourceId == resourceId && b.BlockadeOwnerId == ownerId && !b.ReleasedOnPurpose)
            .ToList()
            .Where(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now)
            .FirstOrDefault() ??
                throw new ResourceCannotBeUnlockedException();

        blockade.Release();
        SaveChanges();
    }

    private void CheckIfActiveBlockade(ResourceId resourceId)
    {
        var activeResourceBlokades = ResourceBlockades
            .Where(b => b.ResourceId == resourceId && !b.ReleasedOnPurpose)
            .ToList();

        if (activeResourceBlokades.Any(b => b.BlockadeDuration == TimeSpan.Zero || b.BlockadeDate.Add(b.BlockadeDuration) > DateTime.Now))
            throw new ResourceAlreadyLockedException();
    }
}
