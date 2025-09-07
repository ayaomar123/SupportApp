
using MediatR;

using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Application.Features.Tickets.Mappers;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Queries.GetTickets
{
    public class GetTicketsQueryHandler(IAppDbContext context) : IRequestHandler<GetTicketsQuery, Result<List<TicketDto>>>
    {
        public async Task<Result<List<TicketDto>>> Handle(GetTicketsQuery request, CancellationToken ct)
        {
            var tickts = await context.Tickets.Include(rt => rt.Category).AsNoTracking().ToListAsync(ct);

            return tickts.ToDtos();
        }
    }
}
