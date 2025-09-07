using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(
    IAppDbContext context,
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
                return ApplicationErrors.CategoryNotFound;
            }

            var imageUrl = await file.UploadAsync(command.Image, "categories", ct);

            var updateResult = category.Update(command.Title, imageUrl, command.Priority);

            if (updateResult.IsError)
            {
                return updateResult.Errors;
            }


            await context.SaveChangesAsync(ct);

            return Result.Updated;
        }
    }
}
