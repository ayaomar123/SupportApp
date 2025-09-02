using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Categories.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public TicketPriority Priority { get; set; }
        public bool IsActive { get; set; }
    }
}
