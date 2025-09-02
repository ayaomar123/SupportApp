using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Entities.Tickets.Categories;

namespace SupportApp.Application.Features.Categories.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new CategoryDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Image = entity.Image,
                Priority = entity.Priority,
                IsActive = entity.IsActive,
            };
        }

        public static List<CategoryDto> ToDtos(this IEnumerable<Category> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

    }
}
