using MediatR;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Api.Commands;

public class DeleteInterfaceCommand : IRequest<DeleteInterfaceResponse>
{
    public DeleteInterfaceCommand(DeleteInterfaceRequest request)
    {
        Request = request;
    }

    public DeleteInterfaceRequest Request { get; private set; }
}