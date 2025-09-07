using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets.Activities
{
    public class TicketActivityError
    {
        public static Error CreatedByTypeRequired =>
           Error.Unauthorized("TicketActivity.CreatedByRole.Required", "TicketActivity CreatedByRole is required");

        public static Error CreatedByUserIdRequired =>
          Error.Unauthorized("TicketActivity.CreatedByUserId.Required", "TicketActivity CreatedByUserId is required");
        public static Error TypeRequired =>
          Error.Validation("TicketActivity.Type.Required", "TicketActivity Type is required");

        public static Error StatusError =>
          Error.Conflict("Ticket.Status.Same", "Ticket Old Status Cant be same as new");
    }
}
