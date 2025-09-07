// TicketActivityAttachmentConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets.Attachments;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class TicketActivityAttachmentConfiguration : IEntityTypeConfiguration<TicketActivityAttachment>
    {
        public void Configure(EntityTypeBuilder<TicketActivityAttachment> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.TicketActivityId).IsRequired();

            builder.Property(c => c.File)
                .IsRequired()
                .HasMaxLength(1024); // optional, but good practice

            builder.HasOne(a => a.TicketActivity)
                .WithMany(ta => ta.Attachments)
                .HasForeignKey(a => a.TicketActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.TicketActivityId);
        }
    }
}
