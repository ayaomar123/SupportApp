using MediatR;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketCommand(
        Guid CategoryId,
        string Title,
        string Description
        ) : IRequest<Result<Guid>>
    {
    }
}
