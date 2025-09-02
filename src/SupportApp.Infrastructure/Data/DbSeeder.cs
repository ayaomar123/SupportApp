using Microsoft.Extensions.DependencyInjection;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Infrastructure.Identity;

namespace SupportApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider,CancellationToken ct)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();


            var admin = User.Create(
                    Guid.NewGuid(),
                    "Administrator2",
                    "admin2@supportapp.com",
                    passwordHasher.Hash("Admin@12345"),
                    "+9705999999978",
                    UserType.Employee);

            if (!admin.IsError)
            {
                context.AppUsers.Add(admin.Value);
                await context.SaveChangesAsync(ct);
             }
        }
    }
}
