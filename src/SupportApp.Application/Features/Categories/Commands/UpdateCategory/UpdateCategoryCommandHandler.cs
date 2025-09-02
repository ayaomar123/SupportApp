using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(
    ILogger<UpdateCategoryCommandHandler> logger,
    IAppDbContext context,
    HybridCache cache,
    IFileStorage file
    )
    : IRequestHandler<UpdateCategoryCommand, Result<Updated>>
    {
        public async Task<Result<Updated>> Handle(UpdateCategoryCommand command, CancellationToken ct)
        {
            var category = await context.Categories
                 .FirstOrDefaultAsync(rt => rt.Id == command.Id, ct);

            if (category is null)
            {
                logger.LogWarning("Category {id} not found for update.", command.Id);

                return ApplicationErrors.CategoryNotFound;
            }

            var imageUrl = await file.UploadAsync(command.Image, "categories", ct);

            var updateResult = category.Update(command.Title, imageUrl, command.Priority);

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
