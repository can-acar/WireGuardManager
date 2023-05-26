using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using NewRelic.LogEnrichers.Serilog;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;
using WireGuardManager.Api.Modules;
using WireGuardManager.Infrastructure.Extensions;
using WireGuardManager.Infrastructure.Utils;

try
{
    var assemblies = Assembly.GetExecutingAssembly();
    var builder = WebApplication.CreateBuilder(args);
    var host = builder.Host;
    var services = builder.Services;

    host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
        .ConfigureLogging(x => x.ClearProviders().AddSerilog())
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

    host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new AppModule());

        //containerBuilder.RegisterDbContext<ApplicationDbContext>("Db");
    });
    builder.WebHost.UseKestrel()
        .UseQuic()
        .UseIISIntegration()
        .UseSockets()
        .UseContentRoot(Directory.GetCurrentDirectory());


    // get namespace  assembly

    services.AddCors();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(
        c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "WireguardManage.API", Version = "v1"}); });
    services.AddOptions();
    services.AddHealthChecks();
    services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    services.AddValidatorsFromAssembly(assemblies);
    services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(assemblies); });


//services.ConfigureInfrastructureServices();
    services.AddSwaggerGen();

    services.Configure<IISOptions>(options => { options.ForwardClientCertificate = false; });
    services.Configure<FormOptions>(o =>
    {
        o.ValueLengthLimit = int.MaxValue;
        o.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
        o.MultipartBoundaryLengthLimit = int.MaxValue;
        o.MultipartHeadersCountLimit = int.MaxValue;
        o.MultipartHeadersLengthLimit = int.MaxValue;
    });

    services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressConsumesConstraintForFormFileParameters = true;
            options.SuppressInferBindingSourcesForParameters = true;
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
            options.ClientErrorMapping[404]
                .Link = "https://httpstatuses.com/404";
        })
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.Formatting = Formatting.None;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
            options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            options.SerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
            options.SerializerSettings.DateFormatString = "dd.MM.yyyy HH:mm:ss";
            options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            options.SerializerSettings.FloatFormatHandling = FloatFormatHandling.String;
            options.SerializerSettings.FloatParseHandling = FloatParseHandling.Double;
        });


    var app = builder.Build();

    ((IApplicationBuilder) app).ApplicationServices.GetAutofacRoot();

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