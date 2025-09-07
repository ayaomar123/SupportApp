using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Api.Requests.Tickets
{
    public sealed class CreateTicketActivityRequest
    {
        public string? Description { get; set; }
        public TicketStatus? NewStatus { get; set; }
        public IFormFileCollection? Files { get; set; }
    }
}
