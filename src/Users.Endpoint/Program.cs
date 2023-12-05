using System.Text.Json.Serialization;
using Users.Endpoint.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
        .AddJsonOptions(opt => { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();
app.MapControllers();
app.Run();