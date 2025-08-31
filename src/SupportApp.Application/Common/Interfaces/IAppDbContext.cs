using Microsoft.EntityFrameworkCore;
using SupportApp.Domain.Entities.Clients;
using SupportApp.Domain.Entities.Employees;
using SupportApp.Domain.Entities.Tickets;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Categories;
using SupportApp.Domain.Entities.Tickets.Notes;
using SupportApp.Domain.Identity;

namespace SupportApp.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Client> Clients { get; }
        public DbSet<Employee> Employees { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }
        public DbSet<Ticket> Tickets { get; }
        public DbSet<Category> Categories { get; }
        public DbSet<TicketActivity> TicketActivities { get; }
        public DbSet<ActivityAttachment> ActivityAttachments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
