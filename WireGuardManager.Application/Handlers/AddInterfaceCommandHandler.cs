using MediatR;
using WireGuardManager.Application.Commands;
using WireGuardManager.Domain.Responses;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Application.Handlers;

internal class AddInterfaceCommandHandler : IRequestHandler<AddInterfaceCommand, AddInterfaceResponse>
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