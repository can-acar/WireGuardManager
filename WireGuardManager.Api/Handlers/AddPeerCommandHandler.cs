using MediatR;
using WireGuardManager.Api.Commands;
using WireGuardManager.Domain.Responses;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Api.Handlers;

public class AddPeerCommandHandler : IRequestHandler<AddPeerCommand, AddPeerResponse>
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