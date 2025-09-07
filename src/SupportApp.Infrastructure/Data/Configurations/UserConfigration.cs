using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SupportApp.Domain.Entities.Identity.User;


namespace SupportApp.Infrastructure.Data.Configurations
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.ToTable("AppUsers");

            builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(c => c.PhoneNumber)
                   .IsUnique(true);

            builder.Property(c => c.Email)
                   .HasMaxLength(150);

            builder.HasIndex(c => c.Email)
                   .IsUnique(true);

            builder.Property(c => c.PasswordHash)
                   .HasMaxLength(150);

            builder.HasMany(c => c.Tickets).WithOne().HasForeignKey(v => v.ReportedByUserId);

            builder.Navigation(c => c.Tickets)
           .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
