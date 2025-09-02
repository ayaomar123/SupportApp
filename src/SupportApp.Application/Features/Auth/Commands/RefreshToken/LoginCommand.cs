using MediatR;

using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Auth.Commands.Login
{
    public sealed record LoginCommand
        (string Email, string Password)
        : IRequest<Result<TokenResponse>>
    {
    }
}
