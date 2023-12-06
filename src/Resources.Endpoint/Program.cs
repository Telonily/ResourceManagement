using Resources.Application;
using Resources.Infrastructure;
using Users.Client.Abstract;
using Users.Client.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddControllers();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UsersEndpointUrl")));

var app = builder.Build();

app.UseInfrastructure();
app.MapControllers();


app.Run();