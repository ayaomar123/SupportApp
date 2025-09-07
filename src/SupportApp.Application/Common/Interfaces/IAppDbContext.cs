using Microsoft.EntityFrameworkCore;

using SupportApp.Domain.Entities.Identity.RefreshToken;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets;
using SupportApp.Domain.Entities.Tickets.Activities;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Categories;

namespace SupportApp.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<User> AppUsers { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }
        public DbSet<Ticket> Tickets { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<TicketActivity> TicketActivities { get; }
        public DbSet<TicketActivityAttachment> TicketActivityAttachments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
