using MediatR;

using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Application.Features.Tickets.Mappers;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Tickets.Queries.GetTicketsById
{
    public class GetTicketsByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetTicketsByIdQuery, Result<TicketDto>>
    {
        public async Task<Result<TicketDto>> Handle(GetTicketsByIdQuery query, CancellationToken ct)
        {
            var ticket = await context
                .Tickets
                .Include(c => c.Category)
                .Include(c => c.Owner)
                .Include(c => c.Assignee)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == query.Id, ct);

            if (ticket is null)
            {
                return ApplicationErrors.TicketNotFound;
            }

            return ticket.ToDto();
        }
    }
}
