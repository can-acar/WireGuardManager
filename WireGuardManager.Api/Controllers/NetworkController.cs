using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class NetworkController : ControllerBase
{
    private readonly IMediator _mediator;

    public NetworkController(IMediator mediator)
    {
        _mediator = mediator;
    }
}