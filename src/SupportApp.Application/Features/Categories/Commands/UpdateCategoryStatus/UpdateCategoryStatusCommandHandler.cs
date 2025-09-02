using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Commands.UpdateCategoryStatus
{
    public class UpdateCategoryStatusCommandHandler(
    ILogger<UpdateCategoryStatusCommandHandler> logger,
    IAppDbContext context,
    HybridCache cache
    )
    : IRequestHandler<UpdateCategoryStatusCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateCategoryStatusCommand command, CancellationToken ct)
        {
            var category = await context.Categories
                 .FirstOrDefaultAsync(rt => rt.Id == command.Id, ct);

            if (category is null)
            {
                logger.LogWarning("Category {id} not found for update.", command.Id);

                return ApplicationErrors.CategoryNotFound;
            }

            var updateResult = category.UpdateStatus();

            if (updateResult.IsError)
            {
                return updateResult.Errors;
            }

            await context.SaveChangesAsync(ct);

            await cache.RemoveByTagAsync("category", ct);

            return Result.Updated;
        }
    }
}
