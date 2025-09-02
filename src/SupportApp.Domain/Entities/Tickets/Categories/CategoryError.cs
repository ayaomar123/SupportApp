using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets.Categories
{
    public class CategoryError
    {
        public static readonly Error TitleRequired =
        Error.Validation("Category.Title.Required", "Category Title is required.");

        public static readonly Error ImageRequired =
        Error.Validation("Category.Image.Required", "Category Image is required.");

        public static readonly Error PriorityRequired =
        Error.Validation("Category.Priority.Required", "Category Priority is required.");
    }
}
