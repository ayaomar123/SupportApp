using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Application.Features.Tickets.Mappers
{
    public static class TicketMapper
    {
        public static TicketDto ToDto(this Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new TicketDto
            {
                Id = entity.Id,
                Number = entity.Number,
                Owner = entity.Owner is null ? null : new CreatorDto
                {
                    Id = entity.OwnerId,
                    Name = entity.Owner.Name,
                    Type = entity.Owner.UserType,
                },
                Category = entity.Category is null ? null : new CategoryMiniDto
                {
                    Id = entity.Category.Id,
                    Title = entity.Category.Title,
                },
                Assignee = entity.Assignee is null ? null : new AssigneeDto
                {
                    Id = (Guid)entity.AssignedToId!,
                    Name = entity.Assignee.Name,
                },
                Title = entity.Title,
                Description = entity.Description,
                Priority = entity.Priority,
                Status = entity.Status,
                // OpenedAt = (DateTime)entity.OpenedAt!,
                // ClosedAt = (DateTime)entity.ClosedAt!,
            };
        }

        public static List<TicketDto> ToDtos(this IEnumerable<Ticket> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

    }
}
