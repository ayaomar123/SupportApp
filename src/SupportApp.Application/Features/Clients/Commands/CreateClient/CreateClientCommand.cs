using MediatR;
using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Clients.Commands.CreateClient
{
    public sealed record CreateClientCommand(
    string Name,
    string PhoneNumber,
    string Email,
    string PasswordHash) : IRequest<Result<ClientDto>>
    {
    }
}
