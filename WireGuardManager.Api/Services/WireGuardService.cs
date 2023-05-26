namespace WireGuardManager.Api.Services;

public class WireGuardService : IWireGuardService
{
    private readonly ILogger<WireGuardService> _logger;

    public WireGuardService(ILogger<WireGuardService> logger)
    {
        _logger = logger;
    }

    public async Task StartInterface(string name)
    {
        throw new NotImplementedException();
    }

    public async Task StopInterface(string name)
    {
        throw new NotImplementedException();
    }

    public async Task RestartInterface(string name)
    {
        throw new NotImplementedException();
    }
}