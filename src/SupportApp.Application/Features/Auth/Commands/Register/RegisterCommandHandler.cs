using MediatR;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler(IIdentityService service) :
        IRequestHandler<RegisterCommand, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(RegisterCommand command, CancellationToken ct)
        {
            var request = new RegisterRequest(
                    command.Name,
                    command.Email,
                    command.Password,
                    command.PhoneNumber,
                    command.UserType);

            return await service.RegisterAsync(request, ct);
        }
    }
}
