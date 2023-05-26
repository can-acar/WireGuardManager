using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Api.Commands;

public record LoginCommand(string requestUsername, string requestPassword) : IRequest<LoginResponse>;