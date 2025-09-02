using MediatR;

using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(Guid Id, string Title, FileUpload Image, TicketPriority Priority) : IRequest<Result<Updated>>
    {
    }
}
