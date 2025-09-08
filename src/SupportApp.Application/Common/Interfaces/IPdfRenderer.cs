using SupportApp.Application.Features.Tickets.Dtos;

namespace SupportApp.Application.Common.Interfaces
{
    public interface IPdfRenderer
    {
        Task<FileResponse> RenderTicketAsync(TicketDto model, CancellationToken ct);
    }
}
