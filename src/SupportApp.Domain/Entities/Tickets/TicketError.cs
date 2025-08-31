using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets
{
    public class TicketError
    {
        public static readonly Error ClientIdRequired =
        Error.Validation("Ticket.ClientId.Required", "Client Id is required.");

        public static readonly Error CategoryIdRequired =
        Error.Validation("Ticket.CategoryId.Required", "Category Id is required.");

        public static readonly Error CategoryIdNotExists =
        Error.Validation("Ticket.CategoryId.NotExists", "Category Id is not exists.");

        public static Error TitleRequired =>
            Error.Validation("Ticket.Title.Required", "Title is required");
    }
}
