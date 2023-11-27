using Resources.Endpoint.ProcessManagers;
using Resources.Endpoint.Resources.Domain.DbContexts;
using Resources.Endpoint.Resources.Domain.Services;

namespace Resources.Endpoint.Resources.Domain.Configuration
{
    public static class Configuration
    {
        public static void ConfigureResourcesDomain(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IResourcesDbContext, ResourcesDbContext>();
            services.Configure<ResourcesDbContextOptions>(o => o.ConnectionString = connectionString);
            services.AddScoped<IResourceManagementService, ResourceManagementService>();
            services.AddScoped<IResourceManagementProcessManager, ResourceManagementProcessManager>();
        }
    }
}
