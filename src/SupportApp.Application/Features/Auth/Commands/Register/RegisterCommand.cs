using MediatR;

using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Features.Auth.Commands.Register
{
    public sealed record RegisterCommand
        (string Name, string Email, string Password, string PhoneNumber, UserType UserType)
        : IRequest<Result<TokenResponse>>
    {
    }
}
