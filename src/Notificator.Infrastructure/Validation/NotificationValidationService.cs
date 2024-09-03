using Notificator.Core.Models;

namespace Notificator.Infrastructure.Validation;

public class NotificationValidationService : INotificationValidationService
{
    private readonly NotificationValidationSettings _settings;

    public NotificationValidationService(NotificationValidationSettings validationSettings)
    {
        _settings = validationSettings;
    }

    public void Validation(NotificationDto notificationDto)
    {
        throw new NotImplementedException();
    }
}