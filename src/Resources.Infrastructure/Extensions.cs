using Microsoft.Extensions.DependencyInjection;
using Resources.Core.Repositories;
using Resources.Endpoint.Availabaility.Domain.DbContexts;
using Resources.Endpoint.Resources.Domain.DbContexts;

namespace Resources.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAvailabilityDbContext, AvailabilityDbContext>();
        services.AddScoped<IResourcesDbContext, ResourcesDbContext>();
        
        return services;
    }
}
