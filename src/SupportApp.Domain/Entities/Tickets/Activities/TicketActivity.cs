using System.ComponentModel.DataAnnotations.Schema;

using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Identity.User;
using SupportApp.Domain.Entities.Tickets.Attachments;
using SupportApp.Domain.Entities.Tickets.Enums;

namespace SupportApp.Domain.Entities.Tickets.Activities
{
    public sealed class TicketActivity : AuditableEntity
    {
        public Guid TicketId { get; private set; }
        public Ticket Ticket { get; private set; } = default!;
        public UserType UserType { get; private set; }
        public Guid CreatedByUserId { get; private set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public User? User { get; private set; }

        public ActivityType Type { get; private set; }
        public string? Description { get; private set; }
        public TicketStatus? OldStatus { get; private set; }
        public TicketStatus? NewStatus { get; private set; }

        private readonly List<TicketActivityAttachment> _attachments = [];
        public IEnumerable<TicketActivityAttachment> Attachments => _attachments.AsReadOnly();

        private TicketActivity() { }

        private TicketActivity(
            Guid id,
            Guid ticketId,
            UserType userType,
            Guid createdByUserId,
            ActivityType type,
            string? description,
            TicketStatus? oldStatus,
            TicketStatus? newStatus)
            : base(id)
        {
            TicketId = ticketId;
            UserType = userType;
            CreatedByUserId = createdByUserId;
            Type = type;
            Description = description;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }

        public static Result<TicketActivity> Create(
            Guid ticketId,
            UserType userType,
            Guid createdByUserId,
            ActivityType type,
            string? description = null,
            TicketStatus? oldStatus = null,
            TicketStatus? newStatus = null)
        {
            if (!Enum.IsDefined(typeof(UserType), userType))
            {
                return TicketActivityError.CreatedByTypeRequired;
            }

            if (createdByUserId == Guid.Empty)
            {
                return TicketActivityError.CreatedByUserIdRequired;
            }

            if (!Enum.IsDefined(typeof(ActivityType), type))
            {
                return TicketActivityError.TypeRequired;
            }

            var activity = new TicketActivity(
                Guid.NewGuid(),
                ticketId,
                userType,
                createdByUserId,
                type,
                description,
                oldStatus,
                newStatus);

            return activity;
        }

        public void AddAttachment(string filePath)
        {
            var result = TicketActivityAttachment.Create(Id, filePath);
            if (result.IsSuccess)
            {
                _attachments.Add(result.Value);
            }
        }
    }
}
