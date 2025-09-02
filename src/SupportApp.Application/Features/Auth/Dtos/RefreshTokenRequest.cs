namespace SupportApp.Application.Features.Auth.Dtos
{
    public sealed record RefreshTokenRequest(string Token, string RefreshToken);
}
