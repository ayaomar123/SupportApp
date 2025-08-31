using SupportApp.Domain.Common;

namespace SupportApp.Domain.Entities.Tickets.Attachments
{
    public class ActivityAttachment : AuditableEntity
    {
        public Guid TicketActivityId { get; private set; }
        public string? File { get; private set; }

        private ActivityAttachment() { }

        private ActivityAttachment(Guid ticketActivityId, string file)
        {
            TicketActivityId = ticketActivityId;
            File = file;
        }
    }
}