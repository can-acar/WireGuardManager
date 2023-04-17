using WireGuardManager.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.ConfigureInfrastructureServices();



var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();