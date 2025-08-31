using SupportApp.Domain.Common.Results;

namespace SupportApp.Domain.Entities.Tickets.Categories
{
    public class CategoryError
    {
        public static readonly Error Title =
        Error.Validation("Category.Title.Required", "Category Title is required.");

        public static readonly Error Image =
        Error.Validation("Category.Image.Required", "Category Image is required.");

        public static readonly Error Priority =
        Error.Validation("Category.Priority.Required", "Category Priority is required.");
    }
}
