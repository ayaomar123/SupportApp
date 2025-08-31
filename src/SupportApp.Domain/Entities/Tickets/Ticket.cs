using SupportApp.Domain.Common;
using SupportApp.Domain.Entities.Tickets.Enums;
using SupportApp.Domain.Entities.Tickets.Notes;

namespace SupportApp.Domain.Entities.Tickets
{
    public class Ticket : AuditableEntity
    {
        public Guid ClientId { get; private set; }
        public Guid CategoryId { get; private set; }
        public int Number { get; private set; } // auto generated
        public string Title { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public TicketPriority Priority { get; private set; }
        public TicketStatus Status { get; private set; } = TicketStatus.New;
        public Guid? AssignedToId { get; private set; }
        public DateTime OpenedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        private readonly List<TicketActivity> _activities = new();
        public IReadOnlyCollection<TicketActivity> Activities => _activities;
    }
}
