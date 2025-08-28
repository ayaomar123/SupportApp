using SupportApp.Domain.Common;

namespace SupportApp.Domain.Entities.Tickets.Notes
{
    public class Note : Entity
    {
        public Guid UserId { get; private set; }

        public string? Description { get; private set; }
        public string? Image { get; private set; }
    }
}
