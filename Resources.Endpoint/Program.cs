using Resources.Endpoint.ProcessManagers;
using Resources.Endpoint.Resources.Domain.Configuration;
using Users.Client.Abstract;
using Users.Client.Concrete;

namespace Resources.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.ConfigureResourcesDomain(builder.Configuration.GetValue<string>("ConnectionString"));
            

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UsersEndpointUrl")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
