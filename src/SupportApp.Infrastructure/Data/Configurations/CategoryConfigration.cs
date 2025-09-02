using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Tickets.Categories;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.Title)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.Image).IsRequired();

            builder.Property(c => c.Priority).IsRequired();

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}
