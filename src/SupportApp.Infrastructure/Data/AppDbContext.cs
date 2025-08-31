using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common;
using SupportApp.Domain.Entities.Clients;
using SupportApp.Domain.Entities.Employees;
using SupportApp.Domain.Entities.Tickets;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Categories;
using SupportApp.Domain.Entities.Tickets.Notes;
using SupportApp.Domain.Identity;
using SupportApp.Infrastructure.Identity;

namespace SupportApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : IdentityDbContext<AppUser>(options), IAppDbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<TicketActivity> TicketActivities => Set<TicketActivity>();
        public DbSet<ActivityAttachment> ActivityAttachments => Set<ActivityAttachment>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
        {
            var domainEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is Entity baseEntity && baseEntity.DomainEvents.Count != 0)
                .Select(e => (Entity)e.Entity)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }

            foreach (var entity in domainEntities)
            {
                entity.ClearDomainEvents();
            }
        }
    }
}
