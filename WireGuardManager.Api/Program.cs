using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using NewRelic.LogEnrichers.Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;
using WireGuardManager.Application.Services;
using WireGuardManager.Infrastructure.Data;
using WireGuardManager.Infrastructure.Extensions;

try
{
    var assemblies = Assembly.GetExecutingAssembly();
    var builder = WebApplication.CreateBuilder(args);
    var host = builder.Host;
    var services = builder.Services;

    host.ConfigureLogging(x => x.ClearProviders().AddSerilog())
        .UseSerilog(((ctx, lc) =>
        {
            if (builder.Environment.IsDevelopment())
            {
                lc.MinimumLevel.Debug();
            }
            else
            {
                lc.MinimumLevel.Information();
            }

            lc.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Internal.WebHost", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting.Server.WebListener", LogEventLevel.Information)
                .Enrich.WithThreadName()
                .Enrich.WithThreadId()
                .Enrich.WithExceptionDetails()
                .Enrich.WithNewRelicLogsInContext()
                .Enrich.WithProperty("ApplicationName", "WireGuard.API")
                .Enrich.FromLogContext()
                .WriteTo.File(@"logs/log-.txt", fileSizeLimitBytes: 3000,
                    rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .ReadFrom.Configuration(ctx.Configuration);
        }));


    services.AddControllers();
    services.AddCors();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(
        c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WireguardManage.API", Version = "v1"}); });
    services.AddOptions();
    services.AddHealthChecks();
    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssembly(assemblies);
    services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });
    services.AddSingleton<IWireGuardService, WireGuardService>();
    services.AddSingleton<ITrafficMonitorService, TrafficMonitorService>();
    services.AddSingleton<ITokenService, TokenService>();
    services.AddDbContext<ApplicationDbContext>();
//services.ConfigureInfrastructureServices();
    services.AddSwaggerGen();


    services.Configure<FormOptions>(o =>
    {
        o.ValueLengthLimit = int.MaxValue;
        o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
        o.MultipartBoundaryLengthLimit = int.MaxValue;
        o.MultipartHeadersCountLimit = int.MaxValue;
        o.MultipartHeadersLengthLimit = int.MaxValue;
    });


    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WireguardManage.API.v1"));
    }

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
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.CloseAndFlush();
}
// services.AddControllers()
//     .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssembly(assemblies); })
//     .AddNewtonsoftJson(options =>
//     {
//         options.SerializerSettings.ContractResolver = new DefaultContractResolver();
//         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//     });