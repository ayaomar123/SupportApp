using FluentValidation;

namespace SupportApp.Application.Features.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
    {
        public RemoveCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("category Id is required.");
        }
    }
}
