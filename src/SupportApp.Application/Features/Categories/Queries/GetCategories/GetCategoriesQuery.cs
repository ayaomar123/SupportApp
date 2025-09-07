using MediatR;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<Result<List<CategoryDto>>>;
}
