using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using WireGuardManager.Domain.Helpers;
using WireGuardManager.Domain.Requests;
using WireGuardManager.Infrastructure.Data;

namespace WireGuardManager.Application.Validators;

public class AddPeerRequestValidator : AbstractValidator<AddPeerRequest>
{
    private readonly ILogger<AddPeerRequestValidator> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public AddPeerRequestValidator(ILogger<AddPeerRequestValidator> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
        // rule for interface name
    }

    public void Generate()
    {
        RuleFor(x => x.Interface.Name)
            .NotEmpty()
            .WithMessage("Interface name cannot be empty")
            .MaximumLength(15)
            .WithMessage("Interface name cannot be longer than 15 characters")
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Interface name can only contain alphanumeric characters and underscores");

        // rule for interface public key
        RuleFor(x => x.Interface.PublicKey)
            .NotEmpty()
            .WithMessage("Interface public key cannot be empty")
            .Length(44)
            .WithMessage("Interface public key must be 44 characters long")
            .Matches(@"^[a-zA-Z0-9/+]+$")
            .WithMessage(
                "Interface public key can only contain alphanumeric characters, plus signs, and forward slashes");

        // rule for interface private key
        RuleFor(x => x.Interface.PrivateKey)
            .NotEmpty()
            .WithMessage("Interface private key cannot be empty")
            .Length(44)
            .WithMessage("Interface private key must be 44 characters long")
            .Matches(@"^[a-zA-Z0-9/+]+$")
            .WithMessage(
                "Interface private key can only contain alphanumeric characters, plus signs, and forward slashes");

        // rule for interface listen port
        // RuleFor(x => x.Interface.ListenPort)
        //     .NotEmpty()
        //     .WithMessage("Interface listen port cannot be empty")
        //     .Matches("^[0-9]+$")
        //     .WithMessage("Interface listen port can only contain numeric characters");
        // .InclusiveBetween(1, 65535, includeMin: true, includeMax: true)
        // .WithMessage("Interface listen port must be between 1 and 65535");

        // rule for interface dns , this is optional
        RuleFor(x => x.Interface.Dns)
            .Matches(@"^[a-zA-Z0-9.]+$")
            .WithMessage("Interface DNS can only contain alphanumeric characters and periods");

        // rule for peer name
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Peer name cannot be empty")
            .MaximumLength(15)
            .WithMessage("Peer name cannot be longer than 15 characters")
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Peer name can only contain alphanumeric characters and underscores");

        // rule for peer endpoint
        RuleFor(x => x.Endpoint)
            .NotEmpty()
            .WithMessage("Peer endpoint cannot be empty")
            .Matches(@"^[a-zA-Z0-9.]+$")
            .WithMessage("Peer endpoint can only contain alphanumeric characters and periods");

        // rule for peer description
        RuleFor(x => x.Description)
            .MaximumLength(255)
            .WithMessage("Peer description cannot be longer than 255 characters");

        // rule for peer nat
        RuleFor(x => x.Nat)
            .NotNull()
            .WithMessage("Peer nat cannot be null");

        // rule for peer persistent keepalive
        RuleFor(x => x.PersistentKeepalive)
            .NotNull()
            .WithMessage("Peer persistent keepalive cannot be null");

        // rule for peer interface id
        // RuleFor(x => x.Interface.Id)
        //     .NotEmpty()
        //     .WithMessage("Peer interface id cannot be empty")
        //     .Matches(@"^[0-9]+$")
        //     .WithMessage("Peer interface id can only contain numeric characters");

        // rule for ipv4 address
        RuleFor(x => x.IpV4Address)
            .NotEmpty()
            .WithMessage("Peer ipv4 address cannot be empty")
            .Matches(@"^[0-9.]+$")
            .WithMessage("Peer ipv4 address can only contain numeric characters and periods");

        // rule for ipv6 address optional
        RuleFor(x => x.IpV6Address)
            .Matches(@"^[a-zA-Z0-9:]+$")
            .WithMessage("Peer ipv6 address can only contain alphanumeric characters and colons");

        // ip address cannot be the same as the interface ip address
        RuleFor(x => x.IpV4Address)
            .Must((request, ipV4Address) => request.Interface.IpV4Address != ipV4Address)
            .WithMessage("Peer ipv4 address cannot be the same as the interface ipv4 address");


        // ip address in range
        RuleFor(x => x.IpV4Address)
            .Must((request, ipV4Address) => NetworkHelper.IsInRange(ipV4Address, request.Interface.IpV4Address))
            .WithMessage("Peer ipv4 address must be in the same range as the interface ipv4 address");
    }
}