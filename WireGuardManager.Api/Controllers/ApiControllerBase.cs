using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class ApiControllerBase : ControllerBase
{
    private readonly ILogger<ApiControllerBase> _ilogger;

    public ApiControllerBase(ILogger<ApiControllerBase> logger)
    {
        _ilogger = logger;
    }

    // GET
}