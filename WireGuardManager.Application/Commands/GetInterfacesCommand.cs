using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public record GetInterfacesCommand(int id) : IRequest<RequestResponse>;