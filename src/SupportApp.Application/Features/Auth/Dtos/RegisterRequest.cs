using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Application.Features.Auth.Dtos
{
    public sealed record RegisterRequest(
    string Name,
    string Email,
    string Password,
    string PhoneNumber,
    UserType UserType);
}
