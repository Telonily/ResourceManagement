using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Resources.Core.Repositories;
using Resources.Infrastructure.DAL.Repositories;

namespace Resources.Infrastructure.DAL;
internal static class Extensions
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        services.AddDbContext<ResourceManagementDbContext>(x => x.UseSqlServer("Data Source=localhost\\sqlexpress;initial catalog=ResourceManagement;integrated security=true;TrustServerCertificate=True"));
        services.AddScoped<IResourceRepository, SqlServerResourceRepository>();
        services.AddScoped<IResourceBlockadesRepository, SqlServerResourceBlockadesRepository>();

        return services;
    }
}
