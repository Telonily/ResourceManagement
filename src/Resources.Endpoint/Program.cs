using Users.Client.Abstract;
using Users.Client.Concrete;
using Resources.Infrastructure;
using Resources.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("UsersEndpointUrl")));

var app = builder.Build();
app.MapControllers();
app.Run();