using Microsoft.Extensions.Logging;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Application.Services;

public class InterfaceService : IInterfaceService
{
    public InterfaceService(ILogger<InterfaceService> logger, ApplicationDbContext dbContext)
    {
    }

    public async Task CreateInterface(string interfaceName)
    {
    }

    public async Task DeleteInterface(string interfaceName)
    {
    }
}