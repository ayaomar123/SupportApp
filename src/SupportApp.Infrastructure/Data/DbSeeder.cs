using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Activities;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Categories;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, CancellationToken ct)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

            const string adminEmail = "admin@supportapp.com";

            var adminExists = await context.AppUsers
                .AnyAsync(u => u.Email == adminEmail, ct);

            if (!adminExists)
            {
                var categoriesToSeed = new[]
                    {
                        Category.Create(Guid.NewGuid(), "Category 1", "img/1.png", TicketPriority.High),
                        Category.Create(Guid.NewGuid(), "Category 2", "img/2.png", TicketPriority.Medium),
                        Category.Create(Guid.NewGuid(), "Category 3", "img/3.png", TicketPriority.Low)
                    };

                foreach (var catResult in categoriesToSeed)
                {
                    var cat = catResult.Value;
                    context.Categories.Add(cat);

                }
                var adminResult = User.Create(
                    Guid.NewGuid(),
                    name: "Administrator",
                    email: adminEmail,
                    passwordHash: passwordHasher.Hash("123456"),
                    phoneNumber: "+9705999999978",
                    userType: UserType.Employee);

                context.AppUsers.Add(adminResult.Value);


                var clientResult = User.Create(
                    Guid.NewGuid(),
                    name: "Client User",
                    email: "client@supportapp.com",
                    passwordHash: passwordHasher.Hash("123456"),
                    phoneNumber: "+9705999999979",
                    userType: UserType.Client);

                context.AppUsers.Add(clientResult.Value);


                var ticketResult = Ticket.Create(
                    reporterId: clientResult.Value.Id,
                    categoryId: categoriesToSeed[0].Value.Id,
                    title: "Sample Ticket",
                    description: "This is a sample ticket description.",
                    priority: TicketPriority.High,
                    assignedToId: adminResult.Value.Id);

                context.Tickets.Add(ticketResult.Value);

                var activity = TicketActivity.Create(
                    ticketResult.Value.Id,
                    UserType.Client,
                    clientResult.Value.Id,
                    ActivityType.Created,
                    description: "Ticket created by client.");

                context.TicketActivities.Add(activity.Value);


                var attachment = TicketActivityAttachment.Create(
                    activity.Value.Id,
                    "img/file.png");

                context.TicketActivityAttachments.Add(attachment.Value);
            }

            await context.SaveChangesAsync(ct);
        }
    }
}
