using MediatR;

using Microsoft.AspNetCore.Mvc;

using SupportApp.Api.Requests.Tickets;
using SupportApp.Application.Common;
using SupportApp.Application.Features.Tickets.Commands.CreateTicket;
using SupportApp.Application.Features.Tickets.Commands.CreateTicketActivity;
using SupportApp.Application.Features.Tickets.Queries.GetTickets;
using SupportApp.Application.Features.Tickets.Queries.GetTicketsById;
using SupportApp.Domain.Common.Results;


namespace SupportApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TicketsController(ISender sender) : ApiController
    {
        [HttpGet(Name = "GetTickets")]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await sender.Send(new GetTicketsQuery(), ct);

            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("{ticketId:guid}", Name = "GetTicketsById")]
        public async Task<IActionResult> GetById(Guid ticketId, CancellationToken ct)
        {
            var result = await sender.Send(new GetTicketsByIdQuery(ticketId), ct);

            return result.Match(response => Ok(response), Problem);
        }

        [HttpPost(Name = "CreateTicket")]
        public async Task<IActionResult> CreateTicket([FromForm] CreateTicketCommand request, CancellationToken ct)
        {
            var result = await sender.Send(request, ct);

            return result.Match(
                response => Ok(response),
                Problem);
        }

        [HttpPost("{ticketId:guid}", Name = "CreateTicketActivity")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(
            [FromRoute] Guid ticketId,
            [FromForm] CreateTicketActivityRequest request,
            CancellationToken ct)
        {
            IEnumerable<FileUpload>? uploads = null;

            if (request.Files is not null && request.Files.Count > 0)
            {
                uploads = request.Files.Select(f => new FileUpload
                {
                    FileName = f.FileName,
                    ContentType = f.ContentType,
                    Length = f.Length,
                    Content = f.OpenReadStream()
                }).ToList();
            }

            var command = new CreateTicketActivityCommand(
                TicketId: ticketId,
                Description: request.Description,
                NewStatus: request.NewStatus,
                Files: uploads
            );

            Result<Guid> result = await sender.Send(command, ct);

            if (result.IsSuccess)
            {
                return CreatedAtAction(
                    actionName: nameof(Create),
                    routeValues: new { ticketId, id = result.Value },
                    value: result.Value
                );
            }

            return result.Match(response => Ok(response), Problem);
        }
    }
}
