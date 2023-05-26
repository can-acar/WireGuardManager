using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Api.Commands;

public record AuthenticateCommand(string requestUsername, string requestPassword) : IRequest<AuthenticateResponse>;