using FluentValidation;
using WireGuardManager.Domain.Requests;

namespace WireGuardManager.Application.Handlers;

public class AddPeerCommandHandler : AbstractValidator<AddPeerRequest>
{
    public AddPeerCommandHandler()
    {
    }
}