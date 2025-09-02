using System.Security.Claims;

namespace SupportApp.Application.Features.Auth.Dtos;

public sealed record AppUserDto(
    string UserId,
    string Email,
    string? Name,
    string UserType,
    IEnumerable<string> Roles);