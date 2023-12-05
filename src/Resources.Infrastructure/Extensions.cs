using Microsoft.Extensions.DependencyInjection;
using Resources.Infrastructure.DAL;

namespace Resources.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqlServer();

        return services;
    }
}
