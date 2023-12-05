using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Resources.Core.Entities;
using Resources.Core.Repositories;

namespace Resources.Endpoint.Resources.Domain.DbContexts;

public class ResourcesDbContextOptions
{
    public required string ConnectionString { get; set; }
}



public class ResourcesDbContext : DbContext, IResourcesDbContext
{
    private readonly ResourcesDbContextOptions Options;

    public ResourcesDbContext(IOptions<ResourcesDbContextOptions> options)
    {
        Options = options.Value;
    }

    public DbSet<Resource> Resources { get; set; }
    ICollection<Resource> IResourcesDbContext.Resources { get => Resources.ToList(); set => throw new NotImplementedException(); }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Options.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Resource");
        base.OnModelCreating(modelBuilder);
    }
}
