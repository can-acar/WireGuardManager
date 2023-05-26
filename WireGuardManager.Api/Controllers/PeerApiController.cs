using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class PeerApiController : ControllerBase
{
    private readonly ILogger<PeerApiController> _logger;

    public PeerApiController(ILogger<PeerApiController> logger)
    {
        _logger = logger;
    }

    // GET
}