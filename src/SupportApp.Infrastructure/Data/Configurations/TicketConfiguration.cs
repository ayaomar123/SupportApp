using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.OwnerId).IsRequired();
            builder.Property(c => c.CategoryId).IsRequired();

            builder.Property(c => c.Number).IsRequired();
            builder.Property(c => c.Title).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(1500);
            builder.Property(c => c.Priority).IsRequired();
            builder.Property(c => c.Status).IsRequired();

            builder.Property(c => c.AssignedToId).IsRequired(false);
            builder.Property(c => c.OpenedAt).IsRequired();
            builder.Property(c => c.ClosedAt).IsRequired(false);

            // ===== العلاقات =====

            builder.HasOne(t => t.Owner)
               .WithMany(u => u.OwnedTickets)      // إن أضفت inverse nav؛ وإلا .WithMany()
               .HasForeignKey(t => t.OwnerId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            builder.HasOne(t => t.Assignee)
                   .WithMany(u => u.AssignedTickets)   // أو .WithMany() إذا ما عندك inverse
                   .HasForeignKey(t => t.AssignedToId)
                   .OnDelete(DeleteBehavior.SetNull)
                   .IsRequired(false);

            // Category
            builder.HasOne(t => t.Category)
                   .WithMany()
                   .HasForeignKey(t => t.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Activities
            builder.HasMany(c => c.Activities)
                   .WithOne(a => a.Ticket)
                   .HasForeignKey(a => a.TicketId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Activities)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            // فهارس
            builder.HasIndex(t => t.CategoryId);
            builder.HasIndex(t => new { t.OwnerId, t.Status });
        }
    }
}
