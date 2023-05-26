using System.Reflection;
using Autofac;
using WireGuardManager.Api.Services;
using Module = Autofac.Module;

namespace WireGuardManager.Api.Modules;

public class AppModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assemblies)
            .Where(what => what.Name.EndsWith("Service"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterAssemblyTypes(assemblies)
            .Where(what => what.Name.EndsWith("Repository"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.RegisterType<WireGuardService>().As<IWireGuardService>().SingleInstance();
        builder.RegisterType<TrafficMonitorService>().As<ITrafficMonitorService>().SingleInstance();
        builder.RegisterType<TokenService>().As<ITokenService>().SingleInstance();
    }
}