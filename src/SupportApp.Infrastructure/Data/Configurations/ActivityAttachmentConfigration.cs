using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets.Attachments;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class ActivityAttachmentConfigration : IEntityTypeConfiguration<ActivityAttachment>
    {
        public void Configure(EntityTypeBuilder<ActivityAttachment> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.TicketActivityId);

            builder.Property(c => c.File).IsRequired();
        }
    }
}
