using MediatR;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SupportApp.Application.Common.Interfaces;
using SupportApp.Domain.Common;
using SupportApp.Domain.Entities.Identity.RefreshToken;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Activities;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Categories;
using SupportApp.Infrastructure.Identity;

namespace SupportApp.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : IdentityDbContext<AppUser>(options), IAppDbContext
    {
        public DbSet<User> AppUsers => Set<User>();
        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<TicketActivity> TicketActivities => Set<TicketActivity>();
        public DbSet<TicketActivityAttachment> TicketActivityAttachments => Set<TicketActivityAttachment>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasSequence<int>("TicketNumbers", schema: "shared")
                .StartsAt(1000)
                .IncrementsBy(1);

            builder.Entity<Ticket>(b =>
            {
                b.Property(t => t.Number)
                 .HasDefaultValueSql("NEXT VALUE FOR shared.TicketNumbers");
            });

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
