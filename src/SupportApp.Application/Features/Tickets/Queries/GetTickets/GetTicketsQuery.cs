using MediatR;

using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Queries.GetTickets
{
    public class GetTicketsQuery : IRequest<Result<List<TicketDto>>>
    {
    }
}
