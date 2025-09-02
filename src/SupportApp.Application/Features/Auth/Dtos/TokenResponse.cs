namespace SupportApp.Application.Features.Auth.Dtos
{
    public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTimeOffset ExpiresOnUtc,
    string UserId,
    string Email,
    string Name,
    string UserType);
}
