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
        var interfaceName = request.Name;


        var command = new AddInterfaceCommand(request);
        var result = await _mediator.Send(command);


        //return CreatedAtAction(nameof(GetInterface), new {interfaceName}, result);
    }

    // GET
}