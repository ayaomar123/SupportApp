using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.RefreshToken;
using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Infrastructure.Identity;

public sealed class IdentityService(
    IAppDbContext context,
    ITokenProvider tokenProvider,
    IPasswordHasher passwordHasher) : IIdentityService
{
    private readonly IAppDbContext _context = context;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    public async Task<Result<TokenResponse>> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        var exists = await _context.AppUsers.AnyAsync(u => u.Email == request.Email, ct);

        if (exists)
        {
            return UserErrors.EmailExists;
        }

        var passwordHash = _passwordHasher.Hash(request.Password);

        var userResult = User.Create(
            Guid.NewGuid(),
            request.Name,
            request.Email,
            passwordHash,
            request.PhoneNumber);

        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var user = userResult.Value;

        _context.AppUsers.Add(user);
        await _context.SaveChangesAsync(ct);

        var userDto = new AppUserDto(
            user.Id.ToString(),
            user.Email!,
            user.Name,
            user.UserType.ToString(),
            new List<string>()
        );

        return await _tokenProvider.GenerateJwtTokenAsync(userDto, ct);
    }

    public async Task<Result<TokenResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _context.AppUsers
            .FirstOrDefaultAsync(u => u.Email == request.Email, ct);

        if (!_passwordHasher.Verify(request.Password, user!.PasswordHash!))
        {
            return UserErrors.PasswordOrEmailError;
        }

        var userDto = new AppUserDto(
            user.Id.ToString(),
            user.Email!,
            user.Name,
            user.UserType.ToString(),
            new List<string>());

        return await _tokenProvider.GenerateJwtTokenAsync(userDto, ct);
    }

    public async Task<Result<TokenResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        var principal = _tokenProvider.GetPrincipalFromExpiredToken(request.Token);
        if (principal is null)
        {
            return RefreshTokenErrors.IdRequired;
        }

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return RefreshTokenErrors.UserIdRequired;
        }

        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Token == request.RefreshToken, ct);

        if (refreshToken is null || refreshToken.ExpiresOnUtc <= DateTime.UtcNow)
        {
            return RefreshTokenErrors.ExpiryInvalid;
        }

        var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Id.ToString() == userId, ct);
        if (user is null)
        {
            return RefreshTokenErrors.UserIdRequired;
        }

        var userDto = new AppUserDto(
            user.Id.ToString(),
            user.Email!,
            user.Name,
            user.UserType.ToString(),
            new List<string>());

        return await _tokenProvider.GenerateJwtTokenAsync(userDto, ct);
    }
}
