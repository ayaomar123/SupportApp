using Microsoft.Extensions.Logging;
using SupportApp.Application.Common.Interfaces;

namespace SupportApp.Infrastructure.Services;

public sealed class NotificationService(ILogger<NotificationService> logger) : INotificationService
{
    private const string Message = "Your Ticket is complete";

    public async Task SendEmailAsync(string to, CancellationToken cancellationToken = default)
    {
        var at = to.IndexOf('@');
        var maskedEmail = at > 1
            ? to[0] + new string('*', at - 2) + to[at - 1] + to[at..]
            : "*****";

        logger.LogInformation("[Email] To: {Email} | Message: {Message}", maskedEmail, Message);

        // Simulated email send
        await Task.CompletedTask;
    }
}