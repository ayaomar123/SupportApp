
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : ICachedQuery<Result<List<CategoryDto>>>
    {
        public string CacheKey => "clients";

        public string[] Tags => ["client"];

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);
    }
}
