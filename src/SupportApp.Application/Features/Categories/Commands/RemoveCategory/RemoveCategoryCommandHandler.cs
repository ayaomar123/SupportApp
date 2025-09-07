using MediatR;
using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandHandler(
        IAppDbContext context
        ) : IRequestHandler<RemoveCategoryCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveCategoryCommand request, CancellationToken ct)
        {
            var category = await context.Categories.FindAsync([request.Id], ct);

            if (category is null)
            {
                return ApplicationErrors.CategoryNotFound;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync(ct);

            return Result.Deleted;
        }
    }
}
