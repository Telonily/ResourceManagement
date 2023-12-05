using Microsoft.EntityFrameworkCore;
using Resources.Core.Entities;

namespace Resources.Infrastructure.DAL;
internal sealed class ResourceManagementDbContext : DbContext
{
    public DbSet<Resource> Resources { get; set; }
    public DbSet<ResourceBlockade> ResourceBlockades { get; set; }

    public ResourceManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
