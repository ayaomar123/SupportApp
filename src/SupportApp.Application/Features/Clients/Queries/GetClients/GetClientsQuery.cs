using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Domain.Common.Results;

namespace SupportApp.Application.Features.Clients.Queries.GetClients
{
    public sealed record GetClientsQuery : ICachedQuery<Result<List<ClientDto>>>
    {
        public string CacheKey => "clients";
        public string[] Tags => ["clients"];

        public TimeSpan Expiration => TimeSpan.FromMinutes(10);
    }
}
