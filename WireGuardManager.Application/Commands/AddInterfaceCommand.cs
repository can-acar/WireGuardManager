using MediatR;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public class AddInterfaceCommand : IRequest<AddInterfaceResponse>
{
    public AddInterfaceRequest Request { get; set; }

    public AddInterfaceCommand(AddInterfaceRequest request)
    {
        Request = request;
    }
}

public class GetAllInterfacesCommand : IRequest<AllInterfacesResponse>
{
    public GetAllInterfacesRequest Request { get; }

    public GetAllInterfacesCommand(GetAllInterfacesRequest request)
    {
        Request = request;
    }
}