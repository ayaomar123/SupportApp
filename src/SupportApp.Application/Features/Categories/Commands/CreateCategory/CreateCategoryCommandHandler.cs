using MediatR;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Categories.Mappers;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets.Categories;

namespace SupportApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler
        (IAppDbContext context,
        IFileStorage file
        ) : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var imageUrl = await file.UploadAsync(request.Image, "categories", ct);

            var createResult = Category.Create(
               id: Guid.NewGuid(),
               title: request.Title,
               image: imageUrl,
               priority: request.Priority);

            if (createResult.IsError)
            {
                return createResult.Errors;
            }

            context.Categories.Add(createResult.Value);

            await context.SaveChangesAsync(ct);

            var category = createResult.Value;

            return category.ToDto();
        }
    }
}
