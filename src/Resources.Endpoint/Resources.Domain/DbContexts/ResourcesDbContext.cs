using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Resources.Endpoint.Resources.Domain.Models;
using System.Reflection.Emit;

namespace Resources.Endpoint.Resources.Domain.DbContexts
{
    public class ResourcesDbContextOptions
    {
        public required string ConnectionString { get; set; }
    }

    public interface IResourcesDbContext
    {
        public DbSet<Resource> Resources { get; set; }
        int SaveChanges();
    }

    public class ResourcesDbContext : DbContext, IResourcesDbContext
    {
        private readonly ResourcesDbContextOptions Options;

        public ResourcesDbContext(IOptions<ResourcesDbContextOptions> options)
        {
            Options = options.Value;
        }

        public DbSet<Resource> Resources { get; set; }

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
}
