using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public record AuthenticateCommand(string requestUsername, string requestPassword) : IRequest<AuthenticateResponse>;