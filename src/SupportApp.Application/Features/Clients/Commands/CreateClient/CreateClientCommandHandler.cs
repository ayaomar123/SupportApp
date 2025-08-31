using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using SupportApp.Application.Common.Interfaces;
using SupportApp.Application.Features.Clients.Dtos;
using SupportApp.Application.Features.Clients.Mappers;
using SupportApp.Domain.Common.Results;
using SupportApp.Domain.Entities.Clients;

namespace SupportApp.Application.Features.Clients.Commands.CreateClient
{
    public sealed class CreateClientCommandHandler
        (IAppDbContext context,
        ILogger<CreateClientCommandHandler> logger,
        HybridCache cache
        ) : IRequestHandler<CreateClientCommand, Result<ClientDto>>
    {
        public async Task<Result<ClientDto>> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            var name = (command.Name ?? string.Empty).Trim();
            var email = (command.Email ?? string.Empty).Trim().ToLowerInvariant();
            var phoneNumber = (command.PhoneNumber ?? string.Empty).Trim();
            var passwordHash = command.PasswordHash;

            var emailExists = await context.Clients.AnyAsync(
            c => c.Email!.ToLower() == email,
            cancellationToken);

            if (emailExists)
            {
                logger.LogWarning("Client creation aborted. Email {email} already exists.", email);

                return ClientErrors.ClientEmailExists;
            }

            var phoneExists = await context.Clients.AnyAsync(
            c => c.PhoneNumber!.ToLower() == phoneNumber,
            cancellationToken);

            if (phoneExists)
            {
                logger.LogWarning("Client creation aborted. phoneNumber {Phone} already exists.", phoneNumber);

                return ClientErrors.ClientPhoneNumberExists;
            }

            var createResult = Client.Create(
               id: Guid.NewGuid(),
               name: name,
               phoneNumber: phoneNumber,
               email: email,
               passwordHash: passwordHash);

            if (createResult.IsError)
            {
                return createResult.Errors;
            }

            context.Clients.Add(createResult.Value);

            await context.SaveChangesAsync(cancellationToken);

            await cache.RemoveByTagAsync("client", cancellationToken);

            var client = createResult.Value;

            logger.LogInformation("Client created successfully. Id: {ClientId}", createResult.Value.Id);

            return client.ToDto();
        }
    }
}