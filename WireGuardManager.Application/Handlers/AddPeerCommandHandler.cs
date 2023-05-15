using MediatR;
using WireGuardManager.Application.Commands;
using WireGuardManager.Domain.Responses;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Application.Handlers;

internal class AddPeerCommandHandler : IRequestHandler<AddPeerCommand, AddPeerResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public AddPeerCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<AddPeerResponse> Handle(AddPeerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}