using OpenStore.Application.Email;
using OpenStore.Infrastructure.Email.Templating;
using OpenStore.Omnichannel.Shared.Dto.Management;

namespace OpenStore.Omnichannel.Infrastructure;

// This class is used by the application to send Email and SMS
// when you turn on two-factor authentication in ASP.NET Identity.
// For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
public class MessageDeliveryService : IMessageDeliveryService
{
    // private const string MainTemplateKey = "MainEmailTemplate";
    private const string NotificationTemplateKey = "NotificationTemplate";

    private readonly IAppEmailSender _appEmailSender;
    private readonly IServiceProvider _serviceProvider;

    public MessageDeliveryService(IAppEmailSender appEmailSender, IServiceProvider serviceProvider)
    {
        _appEmailSender = appEmailSender;
        _serviceProvider = serviceProvider;
    }

    public async Task SendEmailAsync(string email, EmailModel model, CancellationToken cancellationToken = default)
    {
        var mailBuilder = new MailBuilder()
            .AddTo(email)
            .Subject(model.Preview);

        await mailBuilder.UseTemplate(_serviceProvider, NotificationTemplateKey, model);

        await _appEmailSender.SendEmailAsync(mailBuilder, cancellationToken);
    }

    public Task SendSmsAsync(string number, string message, CancellationToken canceledException = default)
    {
        throw new NotSupportedException();
    }

    public Task SendWebPushAsync(NotificationDto notification, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();
    }
}