namespace SupportApp.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(string to, CancellationToken cancellationToken = default);
}