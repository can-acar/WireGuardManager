using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WireGuardManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ApiConventionType(typeof(DefaultApiConventions))]
[Produces(MediaTypeNames.Application.Json)]
public class ApiControllerBase : ControllerBase
{
    private readonly ILogger<ApiControllerBase> _ilogger;
    private readonly IMediator _mediator;

    public ApiControllerBase(ILogger<ApiControllerBase> logger, IMediator mediator)
    {
        _ilogger = logger;
        _mediator = mediator;
    }

    // GET
}