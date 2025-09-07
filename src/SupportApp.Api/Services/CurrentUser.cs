using System.Security.Claims;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Entities.Identity.User;

namespace SupportApp.Api.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public UserType? UserType => Enum.TryParse<UserType>(User?.FindFirstValue("userType"), true, out var ut)
        ? ut
        : null;
}