using MediatR;
using WireGuardManager.Domain.Responses;

namespace WireGuardManager.Application.Commands;

public record GetAllInterfaceCommand(int requestPage, int requestPageSize, string requestSortBy,
    string requestSortOrder, string requestSearch) : IRequest<PaginationResponse>;