using MediatR;
using SupportApp.Application.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Tickets.Commands.CreateTicketActivity
{
    public sealed record CreateTicketActivityCommand(
        Guid TicketId,
        string? Description,
        TicketStatus? NewStatus,
        IEnumerable<FileUpload>? Files
        ) : IRequest<Result<Guid>>
    {
    }
}
