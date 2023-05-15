using Microsoft.Extensions.Logging;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Application.Services;

public interface ITrafficMonitorService
{
}

public class TrafficMonitorService : ITrafficMonitorService
{
    private readonly ILogger<TrafficMonitorService> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public TrafficMonitorService(ILogger<TrafficMonitorService> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public async Task StartAsync()
    {
        throw new NotImplementedException();
    }

    public async Task StopAsync()
    {
        throw new NotImplementedException();
    }
}