using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request, CancellationToken ct = default);

    Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);

    Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default);
}