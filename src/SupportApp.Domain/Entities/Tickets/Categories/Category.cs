using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Domain.Entities.Tickets.Categories
{
    public class Category : AuditableEntity
    {
        public string Title { get; private set; } = default!;
        public string Image { get; private set; } = default!;
        public TicketPriority Priority { get; private set; }
        public bool IsActive { get; private set; } = true;

        public Category()
        {
        }

        public Category(Guid id, string title, string image, TicketPriority priority)
            : base(id)
        {
            Title = title;
            Image = image;
            Priority = priority;
        }

        public static Result<Category> Create(Guid id, string title, string image, TicketPriority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return CategoryError.TitleRequired;
            }

            if (string.IsNullOrWhiteSpace(image))
            {
                return CategoryError.ImageRequired;
            }

            return new Category(id, title, image, priority);
        }

        public Result<Updated> Update(string title, string image, TicketPriority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return CategoryError.TitleRequired;
            }

            if (string.IsNullOrWhiteSpace(image))
            {
                return CategoryError.ImageRequired;
            }

            Title = title;
            Image = image;
            Priority = priority;

            return Result.Updated;
        }

        public void UpdateImage(string imageUrl)
        {
            Image = imageUrl;
        }

        public Result<Updated> UpdateStatus()
        {
            IsActive = !IsActive;

            return Result.Updated;
        }
    }
}
