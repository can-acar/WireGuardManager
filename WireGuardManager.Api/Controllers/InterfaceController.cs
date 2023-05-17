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
    private readonly ILogger<InterfaceController> _logger;
    private readonly IMediator _mediator;

    public InterfaceController(ILogger<InterfaceController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

/*("{interfaceName}"), ProducesResponseType(StatusCodes.Status201Created)*/
    [HttpPost]
    public async Task<IActionResult> AddInterface([FromBody] AddInterfaceRequest request)
    {
        try
        {
            var interfaceName = request.Name;

            var command = new AddInterfaceCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding interface");

            return Problem("Error adding interface", statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPeer([FromBody] AddPeerRequest request)
    {
        try
        {
            var interfaceName = request.Interface.Name;

            var command = new AddPeerCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error adding interface");

            return Problem("Error adding interface", statusCode: 500);
        }

        // GET
    }


    [HttpPost]
    public async Task<IActionResult> DeleteInterface([FromBody] DeleteInterfaceRequest request)
    {
        try
        {
            var command = new DeleteInterfaceCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error deleting interface");

            return Problem("Error deleting interface", statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateInterface([FromBody] UpdateInterfaceRequest request)
    {
        try
        {
            var interfaceName = request.Name;

            var command = new UpdateInterfaceCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating interface");

            return Problem("Error updating interface", statusCode: 500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AllInterfaces([FromBody] GetAllInterfacesRequest request)
    {
        try
        {
            var command = new GetAllInterfacesCommand(request);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting all interfaces");

            return Problem("Error getting all interfaces", statusCode: 500);
        }
    }
}