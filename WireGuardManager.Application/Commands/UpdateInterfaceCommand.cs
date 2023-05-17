using MediatR;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public class UpdateInterfaceCommand : IRequest<UpdateInterfaceResponse>
{
    public UpdateInterfaceRequest Request { get; }


    public UpdateInterfaceCommand(UpdateInterfaceRequest request)
    {
        Request = request;
    }
}