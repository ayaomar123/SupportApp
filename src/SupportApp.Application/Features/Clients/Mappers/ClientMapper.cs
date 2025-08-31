using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Domain.Entities.Clients;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Application.Features.Clients.Mappers
{
    public static class ClientMapper
    {
        public static ClientDto ToDto(this Client entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new ClientDto
            {
                ClientId = entity.Id,
                Name = entity.Name!,
                Email = entity.Email!,
                PhoneNumber = entity.PhoneNumber!,
                Tickets = entity.Tickets?.Select(v => v.ToDto()).ToList() ?? []
            };
        }

        public static List<ClientDto> ToDtos(this IEnumerable<Client> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

        public static TicketDto ToDto(this Ticket entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            return new TicketDto(entity.Id, entity.Title, entity.Description, entity.CreatedAtUtc, entity.Status);
        }

        public static List<TicketDto> ToDtos(this IEnumerable<Ticket> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }
    }
}
