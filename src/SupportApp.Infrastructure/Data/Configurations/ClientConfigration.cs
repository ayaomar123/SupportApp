using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportApp.Domain.Entities.Clients;

namespace SupportApp.Infrastructure.Data.Configurations
{
    public class ClientConfigration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(c => c.Id).IsClustered(false);

            builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(150);

            builder.Property(c => c.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .HasMaxLength(150);

            builder.Property(c => c.PasswordHash)
                   .HasMaxLength(150);

            builder.HasMany(c => c.Tickets).WithOne().HasForeignKey(v => v.ClientId);

            builder.Navigation(c => c.Tickets)
           .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
