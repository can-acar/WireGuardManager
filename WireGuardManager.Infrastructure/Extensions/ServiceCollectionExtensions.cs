using Microsoft.Extensions.DependencyInjection;
using WireGuardManager.Domain.Interfaces;
using WireGuardManager.Infrastructure.Data;
using WireGuardManager.Infrastructure.Repositories;

namespace WireGuardManager.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        // Register repositories
        //services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<IInterfaceRepository, InterfaceRepository>();

        // Register other services, e.g. LoggingMiddleware
        //services.AddTransient<LoggingMiddleware>();

        // register generic unit of work
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        
        

        services.AddScoped<IUnitOfWork<ApplicationDbContext>, UnitOfWork<ApplicationDbContext>>();

        return services;
    }
}