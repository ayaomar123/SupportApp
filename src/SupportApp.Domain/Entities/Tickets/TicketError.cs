using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets
{
    public class TicketError
    {
        public static readonly Error NotFound =
        Error.NotFound("Ticket.id.NotFound", "Ticket Id is NotFound.");

        public static readonly Error AppUserIdRequired =
        Error.Unauthorized("Ticket.AppUserId.Required", "User Id is required.");

        public static readonly Error CategoryIdRequired =
        Error.Validation("Ticket.CategoryId.Required", "Category Id is required.");

        public static readonly Error CategoryIdNotExists =
        Error.Validation("Ticket.CategoryId.NotExists", "Category Id is not exists.");

        public static Error TitleRequired =>
            Error.Validation("Ticket.Title.Required", "Title is required");

        public static Error TicketIsClosed =>
            Error.Validation("Ticket.Title.Closed", "Closed tickets cannot be updated.");


        public static readonly Error TicketConflict =
           Error.Conflict("Ticket.Conflict", "The ticket was modified or deleted by another user. Please reload and try again.");
    }
}
