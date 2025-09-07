using MediatR;

using SupportApp.Application.Common;
using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string Title, FileUpload Image, TicketPriority Priority) : IRequest<Result<CategoryDto>>
    {
    }
}
