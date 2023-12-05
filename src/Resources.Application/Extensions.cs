using Microsoft.Extensions.DependencyInjection;
using Resources.Application.Services;

namespace Resources.Application;
public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IResourceBlockadeProcessManager, ResourceBlockadeProcessManager>();
        services.AddScoped<IResourceManagementProcessManager, ResourceManagementProcessManager>();
        services.AddScoped<IResourceManagementService, ResourceManagementService>();

        return services;
    }
}
