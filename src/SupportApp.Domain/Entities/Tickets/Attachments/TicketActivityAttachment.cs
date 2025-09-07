using System.ComponentModel.DataAnnotations.Schema;

using SupportApp.Domain.Common;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Tickets.Activities;
namespace SupportApp.Domain.Entities.Tickets.Attachments
{
    public class TicketActivityAttachment : AuditableEntity
    {
        public Guid TicketActivityId { get; private set; }

        [ForeignKey(nameof(TicketActivityId))]
        public TicketActivity TicketActivity { get; private set; } = default!;

        public string? File { get; private set; }

        private TicketActivityAttachment() { }

        private TicketActivityAttachment(Guid id, Guid ticketActivityId, string file)
        : base(id)
        {
            TicketActivityId = ticketActivityId;
            File = file;
        }

        public static Result<TicketActivityAttachment> Create(Guid ticketActivityId, string file)
        {
            var attachment = new TicketActivityAttachment(
                id: Guid.NewGuid(),
                ticketActivityId: ticketActivityId,
                file: file
            );

            return attachment;
        }
    }
}