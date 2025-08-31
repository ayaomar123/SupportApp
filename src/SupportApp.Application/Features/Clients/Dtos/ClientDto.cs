namespace SupportApp.Application.Features.Clients.Dtos
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<TicketDto> Tickets { get; set; } = [];
    }
}
