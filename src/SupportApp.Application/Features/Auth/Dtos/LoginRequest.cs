namespace SupportApp.Application.Features.Auth.Dtos
{
    public sealed record LoginRequest(
        string Email,
        string Password);
}
