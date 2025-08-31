using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Application.Features.Clients.Dtos
{
    public sealed record TicketDto(
        Guid TicketId,
        string Title,
        string Description,
        DateTimeOffset CreatedAt,
        TicketStatus Status)
    {
    }
}