using FluentValidation;
namespace SupportApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("Image file is required.");

            RuleFor(x => x.Priority).IsInEnum();

            RuleFor(x => x.Image.Length)
                .LessThanOrEqualTo(3 * 1024 * 1024) // 3MB
                .WithMessage("Maximum allowed file size is 3 MB.");
        }
    }
}
