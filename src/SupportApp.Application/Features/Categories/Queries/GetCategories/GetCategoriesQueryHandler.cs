using MediatR;

using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Categories.Mappers;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler
        (IAppDbContext context,
        IUser authUser) : IRequestHandler<GetCategoriesQuery, Result<List<CategoryDto>>>
    {
        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var query = context.Categories.AsQueryable();


            if (authUser.UserType == UserType.Client)
            {
                query = query.Where(t => t.IsActive);
            }

            var categories = await query.ToListAsync(ct);

            return categories.ToDtos();
        }
    }
}
