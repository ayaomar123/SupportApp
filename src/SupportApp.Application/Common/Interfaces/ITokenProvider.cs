using System.Security.Claims;
using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Common.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}