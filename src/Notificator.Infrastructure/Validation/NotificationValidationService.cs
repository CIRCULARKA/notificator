namespace Notificator.Infrastructure.Validation;

/// <summary>
/// Service for notifications validation
/// </summary>
public class NotificationValidationService : INotificationValidationService
{
    private readonly NotificationValidationSettings _settings;
    
    private readonly ITimeService _timeService;

    public NotificationValidationService(NotificationValidationSettings validationSettings, ITimeService timeService)
    {
        _settings = validationSettings;
        _timeService = timeService;
    }

    /// <summary>
    /// Validates notification. Takes into account validation settings
    /// </summary>
    public void Validation(NotificationDto notificationDto) 
    {
        if (string.IsNullOrWhiteSpace(notificationDto.Text))
            throw new InvalidOperationException("Notification text must not be emtpy");
        if (string.IsNullOrWhiteSpace(notificationDto.Header) && _settings.IsHeaderRequired)
            throw new InvalidOperationException("Notification header must not be empty");
        if (notificationDto.StartTime < _timeService.CurrentTime)
            throw new InvalidOperationException("Notification start time must be greater then current time");
    }
}
