using MediatR;

using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Auth.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand
        (string Token, string RefreshToken)
        : IRequest<Result<TokenResponse>>
    {
    }
}
