using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Api.Requests.Tickets
{
    public class TicketRequest
    {
        public TicketStatus? Status { get; set; }
        public string? Description { get; set; }
        public IFormFile[]? AttachmentFiles { get; set; }
    }
}
