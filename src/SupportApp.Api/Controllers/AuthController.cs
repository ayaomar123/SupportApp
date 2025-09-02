using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportApp.Application.Features.Auth.Commands.Login;
using SupportApp.Application.Features.Auth.Commands.RefreshToken;
using SupportApp.Application.Features.Auth.Commands.Register;

namespace SupportApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ISender sender) : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);

            if (result.IsError)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            return result.Match(response => Ok(response), Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);

            if (result.IsError)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            return result.Match(response => Ok(response), Problem);
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, CancellationToken ct)
        {
            var result = await sender.Send(command, ct);

            if (result.IsError)
            {
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Description).ToList()
                });
            }

            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("employee-data")]
        [Authorize(Policy = "EmployeeOnly")]
        public IActionResult GetEmployeeData()
        {
            return Ok("This is employee-only data.");
        }

        [HttpGet("client-data")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCustomerData()
        {
            return Ok("This is Client-only data.");
        }

    }
}
