using MediatR;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Api.Commands;

public class AddPeerCommand : IRequest<AddPeerResponse>
{
    public AddPeerCommand(AddPeerRequest request)
    {
        Request = request;
    }

    public AddPeerRequest Request { get; set; }
}