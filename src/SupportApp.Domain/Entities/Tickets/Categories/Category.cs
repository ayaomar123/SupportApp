using SupportApp.Domain.Common;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Domain.Entities.Tickets.Categories
{
    public class Category : Entity
    {
        public string Title { get; private set; } = default!;
        public string Image { get; private set; } = default!;
        public TicketPriority Priority { get; private set; }
        public bool IsActive { get; private set; } = true;
    }
}
