using MediatR;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Auth.Dtos;
using SupportApp.Domain.Common.Results;
namespace SupportApp.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IIdentityService service) :
        IRequestHandler<RefreshTokenCommand, Result<TokenResponse>>
    {
        public async Task<Result<TokenResponse>> Handle(RefreshTokenCommand command, CancellationToken ct)
        {
            var request = new RefreshTokenRequest(
                    command.Token,
                    command.RefreshToken);

            return await service.RefreshTokenAsync(request, ct);
        }
    }
}
