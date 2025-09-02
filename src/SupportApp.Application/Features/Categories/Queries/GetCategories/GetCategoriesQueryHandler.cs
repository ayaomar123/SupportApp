using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Categories.Mappers;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(IAppDbContext context) : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
    {
        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync(ct);

            return categories.ToDtos();
        }
    }
}
