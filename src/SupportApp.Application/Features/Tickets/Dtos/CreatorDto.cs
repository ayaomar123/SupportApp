using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Features.Tickets.Dtos
{
    public class CreatorDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public UserType Type { get; set; }
    }

    public class AssigneeDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}