using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WireGuardManager.Api.Commands;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _ilogger;
    private readonly IMediator _mediator;

    public AuthController(ILogger<AuthController> logger,
        IConfiguration configuration, IMediator mediator)
    {
        _ilogger = logger;
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var loginCommand = new LoginCommand(request.Username, request.Password);
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }
        catch (InvalidOperationException e)
        {
            _ilogger.LogError(e, "Error while logging in");
            return BadRequest();
        }
    }


    // GET
}