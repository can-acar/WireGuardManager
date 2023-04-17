using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _ilogger;

    public AuthController(ILogger<AuthController> logger)
    {
        _ilogger = logger;
    }

    // GET
}