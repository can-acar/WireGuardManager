using MediatR;
using WireGuardManager.Api.Commands;
using WireGuardManager.Domain.Responses;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Api.Handlers;

public class AddInterfaceCommandHandler : IRequestHandler<AddInterfaceCommand, AddInterfaceResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public AddInterfaceCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<AddInterfaceResponse> Handle(AddInterfaceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}