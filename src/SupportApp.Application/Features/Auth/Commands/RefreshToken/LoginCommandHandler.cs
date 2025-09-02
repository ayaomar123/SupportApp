using MediatR;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler(IIdentityService service) :
        IRequestHandler<LoginCommand, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(LoginCommand command, CancellationToken ct)
        {
            var request = new LoginRequest(
                    command.Email,
                    command.Password);

            return await service.LoginAsync(request, ct);
        }
    }
}
