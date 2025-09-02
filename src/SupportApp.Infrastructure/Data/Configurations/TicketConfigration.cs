using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class TicketConfigration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.HasKey(c => c.ClientId);

            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Number)
              .IsRequired();

            builder.Property(c => c.Title)
              .IsRequired()
              .HasMaxLength(150);

            builder.Property(c => c.Description)
              .IsRequired()
              .HasMaxLength(1500);

            builder.Property(c => c.Priority)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(c => c.Status)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(c => c.AssignedToId)
              .IsRequired();

            builder.Property(c => c.OpenedAt)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(c => c.ClosedAt)
              .IsRequired()
              .HasMaxLength(50);

            builder.HasMany(c => c.Activities).WithOne().HasForeignKey(v => v.Id);

            builder.Navigation(c => c.Activities)
           .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
