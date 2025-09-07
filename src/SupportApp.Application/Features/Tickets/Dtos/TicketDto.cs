using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Tickets.Dtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public CreatorDto? Owner { get; set; }
        public CategoryMiniDto? Category { get; set; }
        public AssigneeDto? Assignee { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime ClosedAt { get; set; }
        public List<TicketActivityDto> Activities { get; set; } = [];

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
