using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Application.Features.Tickets.Mappers;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Features.Tickets.Queries.GetTickets
{
    public class GetTicketsQueryHandler
        (IAppDbContext context,
        IUser authUser) : IRequestHandler<GetTicketsQuery, Result<List<TicketDto>>>
    {
        public async Task<Result<List<TicketDto>>> Handle(GetTicketsQuery request, CancellationToken ct)
        {
            var query = context.Tickets.AsQueryable();

            if (authUser.UserType == UserType.Client)
            {
                query = query.Where(t => t.ReportedByUserId == Guid.Parse(authUser.Id!));
            }

            var tickets = await query
                .Include(t => t.Category)
                .Include(t => t.ReportedBy)
                .Include(t => t.Assignee)
                .ToListAsync(ct);

            return tickets.ToDtos();
        }
    }
}
