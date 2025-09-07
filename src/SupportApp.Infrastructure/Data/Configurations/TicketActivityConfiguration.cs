// TicketActivityConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets.Activities;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class TicketActivityConfiguration : IEntityTypeConfiguration<TicketActivity>
    {
        public void Configure(EntityTypeBuilder<TicketActivity> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.TicketId).IsRequired();
            builder.Property(c => c.UserType).IsRequired();
            builder.Property(c => c.CreatedByUserId).IsRequired();
            builder.Property(c => c.Type).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(1000);
            builder.Property(c => c.OldStatus);
            builder.Property(c => c.NewStatus);

            builder.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Ticket)
                .WithMany(t => t.Activities)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Attachments)
                .WithOne(a => a.TicketActivity)
                .HasForeignKey(a => a.TicketActivityId) // ✅ FIXED
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Attachments)
                   .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasIndex(a => a.TicketId);
        }
    }
}
