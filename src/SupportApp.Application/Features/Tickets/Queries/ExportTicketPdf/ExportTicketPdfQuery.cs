using MediatR;
using SupportApp.Application.Features.Tickets.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Tickets.Queries.ExportTicketPdf
{
    public sealed record ExportTicketPdfQuery(
        Guid TicketId
    ) : IRequest<Result<FileResponse>>;
}
