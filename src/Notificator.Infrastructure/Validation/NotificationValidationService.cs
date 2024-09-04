namespace Notificator.Infrastructure.Validation;

/// <summary>
/// Service for notifications validation
/// </summary>
public class NotificationValidationService : INotificationValidationService
{
    private readonly NotificationValidationSettings _settings;

    public NotificationValidationService(NotificationValidationSettings validationSettings)
    {
        _settings = validationSettings;
    }

    /// <summary>
    /// Validates notification. Takes into account validation settings
    /// </summary>
    public void Validation(NotificationDto notificationDto) 
    {
        if (string.IsNullOrWhiteSpace(notificationDto.Text))
            throw new InvalidOperationException("Notification text must not be emtpy");
    }
}
