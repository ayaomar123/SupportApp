using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SupportApp.Domain.Entities.Identity.User;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> b)
    {
        b.HasKey(c => c.Id).IsClustered(false);

        b.Property(c => c.ReportedByUserId).IsRequired();
        b.Property(c => c.AssignedToId).IsRequired(false);
        b.Property(c => c.CategoryId).IsRequired();

        b.Property(c => c.Title).IsRequired().HasMaxLength(150);
        b.Property(c => c.Description).IsRequired().HasMaxLength(1500);
        b.Property(c => c.Priority).IsRequired();
        b.Property(c => c.Status).IsRequired();
        b.Property(c => c.OpenedAt).IsRequired();
        b.Property(c => c.ClosedAt).IsRequired(false);

        // ⬇⬇ أهم سطرين: هذا يمنع EF من اختراع ReportedById
        b.HasOne(t => t.ReportedBy)
         .WithMany(u => u.ReportedTickets)
         .HasForeignKey(t => t.ReportedByUserId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasOne(t => t.Assignee)
         .WithMany(u => u.AssignedTickets)
         .HasForeignKey(t => t.AssignedToId)
         .OnDelete(DeleteBehavior.SetNull);

        b.HasOne(t => t.Category)
         .WithMany()
         .HasForeignKey(t => t.CategoryId)
         .OnDelete(DeleteBehavior.Restrict);

        b.HasMany(c => c.Activities)
         .WithOne(a => a.Ticket)
         .HasForeignKey(a => a.TicketId)
         .OnDelete(DeleteBehavior.Cascade);

        b.Navigation(c => c.Activities).UsePropertyAccessMode(PropertyAccessMode.Field);

        b.HasIndex(t => new { t.ReportedByUserId, t.Status });
        b.HasIndex(t => t.CategoryId);
    }
}
