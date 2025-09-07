
using MediatR;

using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler
        (IAppDbContext context,
        IUser user
        ) : IRequestHandler<CreateTicketCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateTicketCommand request, CancellationToken ct)
        {
            if (!Guid.TryParse(user.Id, out var appUserId))
            {
                return TicketError.AppUserIdRequired;
            }

            var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.CategoryId, ct);

            if (category is null)
            {
                return TicketError.CategoryIdRequired;
            }

            var assinee = await context.AppUsers.OrderBy(u => Guid.NewGuid()).FirstOrDefaultAsync(ct);

            var createResult = Ticket.Create(
               reporterId: appUserId,
               categoryId: request.CategoryId,
               title: request.Title,
               description: request.Description,
               priority: category!.Priority,
               assignedToId: assinee!.Id);

            if (createResult.IsError)
            {
                return createResult.Errors;
            }

            context.Tickets.Add(createResult.Value);

            await context.SaveChangesAsync(ct);

            var ticket = createResult.Value;

            return ticket.Id;
        }
    }
}
