using MediatR;
using Microsoft.EntityFrameworkCore;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Application.Features.Clients.Mappers;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Clients.Queries.GetClients
{
    public class GetClientsQueryHandler(IAppDbContext context) : IRequestHandler<GetClientsQuery, Result<List<ClientDto>>>
    {
        public async Task<Result<List<ClientDto>>> Handle(GetClientsQuery query, CancellationToken ct)
        {
            var clients = await context.Clients.Include(c => c.Tickets).AsNoTracking().ToListAsync(ct);

            return clients.ToDtos();
        }
    }
}
