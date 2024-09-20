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
        if (notificationDto.MaxAmount < 0)
            throw new InvalidOperationException("Notificator max amount can`t be less then 0");
        if (notificationDto.Interval != null || notificationDto.Interval < _settings.MinInterval)
            throw new InvalidOperationException("Notificator interval must be empty or interval value must be greater then MinInterval");
        //if (notificationDto.DaysOfTheWeek != null || )

    }
}
