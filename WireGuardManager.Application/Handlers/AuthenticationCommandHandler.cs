using FluentValidation;
using WireGuardManager.Domain.Requests;

namespace WireGuardManager.Application.Handlers;

public class AuthenticationCommandHandler : AbstractValidator<LoginRequest>
{
    public AuthenticationCommandHandler()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}