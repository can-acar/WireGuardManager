using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WireGuardManager.Application.Commands;
using WireGuardManager.Domain.Requests;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class InterfaceController : ControllerBase
{
    private readonly ILogger<InterfaceController> _ilogger;
    private readonly IMediator _mediator;

    public InterfaceController(ILogger<InterfaceController> logger, IMediator mediator)
    {
        _ilogger = logger;
        _mediator = mediator;
    }

    [HttpPost("")]
    public async Task<IActionResult> Interface([FromBody] GetAllInterfacesRequest request)
    {
        try
        {
            var cmd = new GetAllInterfaceCommand(request.Page, request.PageSize, request.SortBy, request.SortOrder,
                request.Search);
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }
        catch (InvalidOperationException e)
        {
            _ilogger.LogError(e, e.Message);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInterface([FromRoute] int id)
    {
        try
        {
            var cmd = new GetInterfacesCommand(id);
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return NotFound(result.Message);

            return Ok(result);
        }
        catch (InvalidOperationException e)
        {
            _ilogger.LogError(e, e.Message);
            return BadRequest();
        }
    }


    // GET
}