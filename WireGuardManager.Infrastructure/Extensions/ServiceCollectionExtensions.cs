using Microsoft.Extensions.DependencyInjection;
namespace WireGuardManager.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IInterfaceRepository, InterfaceRepository>();

        // Register other services, e.g. LoggingMiddleware
        services.AddTransient<LoggingMiddleware>();

        return services;
    }
}