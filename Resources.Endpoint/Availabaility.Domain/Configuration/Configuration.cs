using Resources.Endpoint.Availabaility.Domain.DbContexts;

namespace Resources.Endpoint.Availabaility.Domain.Configuration
{
    public static class Configuration
    {
        public static void ConfigureAvailabilityDomain(this IServiceCollection services, string connectionString, int blockadeTimeInMinutes)
        {
            services.AddScoped<IAvailabilityDbContext, AvailabilityDbContext>();
            services.Configure<AvailabilityDbContextOptions>(o => o.ConnectionString = connectionString);
        }
    }
}
