using MediatR;

using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Queries.GetTicketsById
{
    public sealed record GetTicketsByIdQuery(Guid Id) : IRequest<Result<TicketDto>>
    {
    }
}
