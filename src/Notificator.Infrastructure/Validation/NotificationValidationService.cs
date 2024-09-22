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
            throw new InvalidOperationException("Notification's text must not be empty");
        if (string.IsNullOrWhiteSpace(notificationDto.Header) && _settings.IsHeaderRequired)
            throw new InvalidOperationException("Notification's header must not be empty");
        if (notificationDto.StartTime > _timeService.CurrentTime)
            throw new InvalidOperationException("Notification's start time must be greater then current time");
        if (notificationDto.MaxAmount < 0)
            throw new InvalidOperationException("Max amount of notifications must be greater than zero");
        if (notificationDto.Interval != null || notificationDto.Interval < _settings.MinInterval)
            throw new InvalidOperationException("Notification's interval must not be less than the minimal interval of {_settings.MinInterval}");
        if (notificationDto.EndTime != null && notificationDto.EndTime < notificationDto.StartTime)
            throw new InvalidOperationException("Notification's end time must be greater then start time");
        if ((notificationDto.StartTime - notificationDto.EndTime) < _settings.MinInterval)
            throw new InvalidOperationException("Notification's {_settings.MinInterval} must be greater then difference between start time and end time");
    }
}