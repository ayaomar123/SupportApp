using Microsoft.AspNetCore.Identity;

namespace SupportApp.Infrastructure.Identity;

public sealed class AppUser : IdentityUser
{
    public string? Name { get; set; }
}