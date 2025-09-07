using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Tickets.Dtos
{
    public class TicketActivityDto
    {
        public Guid Id { get; set; }

        public ActivityType? Type { get; set; }
        public string? Note { get; set; }

        public CreatorDto? Creator { get; set; }

        public string? Description { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public List<TicketActivityAttachmentDto> Attachments { get; set; } = [];
    }

}
