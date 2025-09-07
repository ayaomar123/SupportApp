using SupportApp.Application.Features.Categories.Dtos;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Domain.Entities.Tickets.Activities;

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
                Category = entity.Category is null ? null : new CategoryMiniDto
                {
                    Id = entity.Category.Id,
                    Title = entity.Category.Title,
                },
                Owner = entity.ReportedBy is null ? null : new CreatorDto
                {
                    Id = entity.ReportedByUserId,
                    Name = entity.ReportedBy.Name,
                    Type = entity.ReportedBy.UserType,
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
                OpenedAt = entity.OpenedAt,
                CreatedAt = entity.CreatedAtUtc,
                UpdatedAt = entity.LastModifiedUtc,
                ClosedAt = entity.ClosedAt is null ? default : (DateTime)entity.ClosedAt,
                Activities = entity.Activities
                    .Select(a => a.ToActivityDto())
                    .ToList()
            };
        }

        public static List<TicketDto> ToDtos(this IEnumerable<Ticket> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

        public static TicketActivityDto ToActivityDto(this TicketActivity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new TicketActivityDto
            {
                Id = entity.Id,
                Type = entity.Type,
                Note = entity.ToString(),
                Description = entity.Description,
                Creator = entity.User is null ? null : new CreatorDto
                {
                    Id = entity.CreatedByUserId,
                    Name = entity.User!.Name,
                    Type = entity.UserType,
                },
                CreatedAtUtc = entity.CreatedAtUtc,
                Attachments = entity.Attachments
                    .Select(a => new TicketActivityAttachmentDto
                    {
                        Id = a.Id,
                        File = a.File
                    })
                    .ToList(),
            };
        }

    }
}
