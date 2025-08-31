using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets.Notes
{
    public class TicketActivityError
    {
        public static Error CreatedByRoleRequired =>
           Error.Validation("TicketActivity.CreatedByRole.Required", "TicketActivity CreatedByRole is required");

        public static Error CreatedByUserIdRequired =>
          Error.Validation("TicketActivity.CreatedByUserId.Required", "TicketActivity CreatedByUserId is required");
        public static Error TypeRequired =>
          Error.Validation("TicketActivity.Type.Required", "TicketActivity Type is required");
    }
}
