using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets.Notes;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class TicketActivityConfigration : IEntityTypeConfiguration<TicketActivity>
    {
        public void Configure(EntityTypeBuilder<TicketActivity> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.CreatedByRole)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.CreatedByUserId)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.Type)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.Description)
               .IsRequired()
               .HasMaxLength(1000);

            builder.Property(c => c.OldStatus)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.NewStatus)
               .IsRequired()
               .HasMaxLength(150);

            builder.HasMany(c => c.Attachments).WithOne().HasForeignKey(v => v.TicketActivityId).OnDelete(DeleteBehavior.Cascade);

            builder.Navigation(c => c.Attachments)
           .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
