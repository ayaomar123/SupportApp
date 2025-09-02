using MediatR;

using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Categories.Commands.UpdateCategoryStatus
{
    public sealed record UpdateCategoryStatusCommand(Guid Id) : IRequest<Result<Updated>>
    {
    }
}
