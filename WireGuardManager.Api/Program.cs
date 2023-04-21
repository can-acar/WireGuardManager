using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using WireGuardManager.Application.Services;
using WireGuardManager.Infrastructure.Data;
using WireGuardManager.Infrastructure.Extensions;

var assemblies = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddCors();
services.AddEndpointsApiExplorer();
services.AddOptions();
services.AddHealthChecks();
services.AddFluentValidationAutoValidation();
services.AddValidatorsFromAssembly(assemblies);
services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
services.ConfigureInfrastructureServices();
services.AddSingleton<IWireGuardService, WireGuardService>();
services.AddSingleton<ITrafficMonitorService, TrafficMonitorService>();
services.AddSingleton<ITokenService, TokenService>();
services.AddDbContext<ApplicationDbContext>();


services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
    o.MultipartBoundaryLengthLimit = int.MaxValue;
    o.MultipartHeadersCountLimit = int.MaxValue;
    o.MultipartHeadersLengthLimit = int.MaxValue;
});


var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseAppMiddleware();

app.UseWebSockets();

app.UseRouting();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true) // allow any origin
    .AllowCredentials());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();