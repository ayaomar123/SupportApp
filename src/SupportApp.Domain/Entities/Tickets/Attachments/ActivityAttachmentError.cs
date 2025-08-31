
using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets.Attachments
{
    public class ActivityAttachmentError
    {
        public static Error TicketActivityIdRequired =>
           Error.Validation("ActivityAttachment.TicketActivityId.Required", "ActivityAttachment TicketActivityId is required");

        public static Error TicketActivityIdNotExists =>
           Error.Validation("ActivityAttachment.TicketActivityId.NotExists", "ActivityAttachment TicketActivityId is NotExists");
    }
}