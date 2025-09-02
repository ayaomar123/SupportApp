using System.ComponentModel.DataAnnotations;

using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Api.Requests.Categories
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Title is required...")]
        public string Title { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public TicketPriority Priority { get; set; }
    }
}
