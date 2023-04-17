using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class InterfaceController : ControllerBase
{
    private readonly ILogger<InterfaceController> _ilogger;

    public InterfaceController(ILogger<InterfaceController> logger)
    {
        _ilogger = logger;
    }

    // GET
}