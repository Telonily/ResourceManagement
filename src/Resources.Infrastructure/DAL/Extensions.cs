using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resources.Core.Repositories;
using Resources.Infrastructure.DAL.Repositories;

namespace Resources.Infrastructure.DAL;
internal static class Extensions
{
    private const string SectionName = "database";

    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<SqlServerOptions>(section);
        var options = configuration.GetOptions<SqlServerOptions>(SectionName);


        services.AddDbContext<ResourceManagementDbContext>(x => x.UseSqlServer(options.ConnectionString));
        services.AddScoped<IResourceRepository, SqlServerResourceRepository>();
        services.AddScoped<IResourceBlockadesRepository, SqlServerResourceBlockadesRepository>();

        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}
