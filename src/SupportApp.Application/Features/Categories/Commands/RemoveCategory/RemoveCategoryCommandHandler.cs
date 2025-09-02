using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Categories.Commands.RemoveCategory
{
    public class RemoveCategoryCommandHandler(
        IAppDbContext context,
        ILogger<RemoveCategoryCommandHandler> logger,
        HybridCache cache
        ) : IRequestHandler<RemoveCategoryCommand, Result<Deleted>>
    {
        public async Task<Result<Deleted>> Handle(RemoveCategoryCommand request, CancellationToken ct)
        {
            var category = await context.Categories.FindAsync([request.Id], ct);

            if (category is null)
            {
                logger.LogWarning("Category with id {CategoryId} not found for deletion.", request.Id);
                return ApplicationErrors.CategoryNotFound;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync(ct);

            await cache.RemoveByTagAsync("category", ct);
            logger.LogInformation("Category {CategoryId} deleted successfully.", request.Id);
            return Result.Deleted;
        }
    }
}
