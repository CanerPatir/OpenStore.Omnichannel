using OpenStore.Omnichannel.Shared.Dto.Management;

namespace OpenStore.Omnichannel.Infrastructure;

public interface IMessageDeliveryService
{
    Task SendEmailAsync(string email, EmailModel model, CancellationToken cancellationToken = default);

    Task SendSmsAsync(string number, string message, CancellationToken canceledException = default);

    Task SendWebPushAsync(NotificationDto notification, CancellationToken cancellationToken = default);
}