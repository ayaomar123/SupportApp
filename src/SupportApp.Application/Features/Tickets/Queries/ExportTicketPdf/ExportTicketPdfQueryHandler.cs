using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Errors;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Application.Features.Tickets.Mappers;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Queries.ExportTicketPdf
{
    public class ExportTicketPdfQueryHandler(
        IAppDbContext context,
        IPdfRenderer pdfRenderer)
        : IRequestHandler<ExportTicketPdfQuery, Result<FileResponse>>
    {
        public async Task<Result<FileResponse>> Handle(ExportTicketPdfQuery query, CancellationToken ct)
        {
            var ticket = await context.Tickets
                .Include(t => t.Category)
                .Include(t => t.ReportedBy)
                .Include(t => t.Assignee)
                .Include(t => t.Activities)
                    .ThenInclude(a => a.User)
                .Include(t => t.Activities)
                    .ThenInclude(a => a.Attachments)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == query.TicketId, ct);

            if (ticket is null)
            {
                return ApplicationErrors.TicketNotFound;
            }

            TicketDto dto = ticket.ToDto();

            var pdf = await pdfRenderer.RenderTicketAsync(dto, ct);

            return pdf;
        }
    }
}
