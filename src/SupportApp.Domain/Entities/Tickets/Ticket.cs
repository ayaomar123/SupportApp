using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets;
using SupportApp.Domain.Entities.Tickets.Activities;
using SupportApp.Domain.Entities.Tickets.Categories;
using SupportApp.Domain.Entities.Tickets.Enums;

public sealed class Ticket : AuditableEntity
{
    public Guid ReportedByUserId { get; private set; }
    public User? ReportedBy { get; private set; }

    public Guid? AssignedToId { get; private set; }
    public User? Assignee { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category? Category { get; private set; }

    public int Number { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public TicketPriority Priority { get; private set; }
    public TicketStatus Status { get; private set; } = TicketStatus.New;

    public DateTime OpenedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }

    private readonly List<TicketActivity> _activities = [];
    public IEnumerable<TicketActivity> Activities => _activities.AsReadOnly();

    private Ticket() { }

    private Ticket(
        Guid id,
        Guid reporterId,
        Guid categoryId,
        string title,
        string description,
        TicketPriority priority,
        Guid? assignedToId) : base(id)
    {
        ReportedByUserId = reporterId;
        CategoryId = categoryId;
        Title = title;
        Description = description;
        Priority = priority;
        Status = TicketStatus.New;
        OpenedAt = DateTime.UtcNow;
        AssignedToId = assignedToId;
    }

    public static Result<Ticket> Create(
        Guid reporterId,
        Guid categoryId,
        string title,
        string description,
        TicketPriority priority,
        Guid? assignedToId = null)
    {
        if (reporterId == Guid.Empty) return TicketError.AppUserIdRequired;
        if (categoryId == Guid.Empty) return TicketError.CategoryIdRequired;
        if (string.IsNullOrWhiteSpace(title)) return TicketError.TitleRequired;

        return new Ticket(
            id: Guid.NewGuid(),
            reporterId: reporterId,
            categoryId: categoryId,
            title: title.Trim(),
            description: description?.Trim() ?? string.Empty,
            priority: priority,
            assignedToId: assignedToId
        );
    }
}
