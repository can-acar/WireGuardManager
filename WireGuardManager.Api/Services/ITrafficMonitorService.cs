namespace WireGuardManager.Api.Services;

public interface ITrafficMonitorService
{
    Task StartAsync();

    Task StopAsync();
}