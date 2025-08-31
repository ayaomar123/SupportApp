using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Application.Features.Clients.Queries.GetClients;

namespace SupportApp.Api.Controllers;

[Route("api/clients")]
// [Authorize]
public sealed class ClientsController(ISender sender) : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [EndpointSummary("Retrieves a list of customers.")]
    [EndpointDescription("Returns all customers associated with the current user.")]
    [EndpointName("GetClients")]
    [ProducesDefaultResponseType]
    [OutputCache(Duration = 60)]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var result = await sender.Send(new GetClientsQuery(), ct);

        return result.Match(
            response => Ok(response),
            Problem);
    }
}