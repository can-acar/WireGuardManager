using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public record LoginCommand(string requestUsername, string requestPassword) : IRequest<LoginResponse>;