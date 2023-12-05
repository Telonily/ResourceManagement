using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resources.Infrastructure.DAL;

namespace Resources.Infrastructure;
public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlServer(configuration);

        return services;
    }
}
