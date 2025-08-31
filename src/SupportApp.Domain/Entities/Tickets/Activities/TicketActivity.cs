using SupportApp.Domain.Common;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Domain.Entities.Tickets.Notes
{
    public class TicketActivity : AuditableEntity
    {
        public ActorRole CreatedByRole { get; private set; }
        public Guid CreatedByUserId { get; private set; }
        public ActivityType Type { get; private set; }
        public string? Description { get; private set; }
        public TicketStatus? OldStatus { get; private set; }
        public TicketStatus? NewStatus { get; private set; }

        private readonly List<ActivityAttachment> _attachments = new();
        public IReadOnlyCollection<ActivityAttachment> Attachments => _attachments;
    }
}
