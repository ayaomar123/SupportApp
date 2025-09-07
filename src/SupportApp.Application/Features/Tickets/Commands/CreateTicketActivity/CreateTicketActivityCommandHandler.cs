using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets;
using SupportApp.Domain.Entities.Tickets.Activities;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Tickets.Commands.CreateTicketActivity
{
    public class CreateTicketActivityCommandHandler(
        IAppDbContext context,
        IFileStorage files,
        IUser AuthUser)
        : IRequestHandler<CreateTicketActivityCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateTicketActivityCommand cmd, CancellationToken ct)
        {
            var ticket = await context.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == cmd.TicketId, ct);

            if (ticket is null)
            {
                return TicketError.NotFound;
            }

            if (!Guid.TryParse(AuthUser.Id, out var userId))
            {
                return TicketActivityError.CreatedByUserIdRequired;
            }

            if (cmd.NewStatus.HasValue && cmd.NewStatus.Value == ticket.Status)
            {
                return TicketActivityError.StatusError;
            }

            var userType = (UserType)AuthUser.UserType!;

            var hasFiles = cmd.Files is { } && cmd.Files.Any();

            ActivityType type =
                cmd.NewStatus.HasValue ? ActivityType.StatusChanged :
                hasFiles ? ActivityType.Attachment :
                (userType == UserType.Employee ? ActivityType.InternalNote : ActivityType.Comment);

            var oldStatus = ticket.Status;

            var createResult = TicketActivity.Create(
                ticketId: cmd.TicketId,
                userType: userType,
                createdByUserId: userId,
                type: type,
                description: cmd.Description,
                oldStatus: oldStatus,
                newStatus: cmd.NewStatus
            );

            if (!createResult.IsSuccess)
            {
                return createResult.Errors;
            }

            var activity = createResult.Value;

            if (cmd.Files is not null)
            {
                var baseFolder = $"tickets/{cmd.TicketId}/activities/{activity.Id}";

                foreach (var file in cmd.Files)
                {
                    var storedPath = await files.UploadAsync(file, baseFolder, ct);
                    activity.AddAttachment(storedPath);
                }
            }

            context.TicketActivities.Add(activity);

            await context.SaveChangesAsync(ct);

            return activity.Id;

        }
    }
}
