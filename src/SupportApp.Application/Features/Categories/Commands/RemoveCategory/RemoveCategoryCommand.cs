using MediatR;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Categories.Commands.RemoveCategory
{
    public sealed record RemoveCategoryCommand(Guid Id)
    : IRequest<Result<Deleted>>;
}
