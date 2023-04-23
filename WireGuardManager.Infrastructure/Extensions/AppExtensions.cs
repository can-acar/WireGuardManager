using Microsoft.AspNetCore.Builder;

namespace WireGuardManager.Infrastructure.Extensions;

public static class AppExtensions
{
    public static WebApplication UseAppMiddleware(this WebApplication app)
    {
        return app;
    }
    
    
}

