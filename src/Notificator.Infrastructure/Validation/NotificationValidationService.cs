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
    public void Validate(NotificationDto notificationDto) 
    {
        if (string.IsNullOrWhiteSpace(notificationDto.Text))
            throw new InvalidOperationException("Notification Text must not be emtpy");
        if (string.IsNullOrWhiteSpace(notificationDto.Header) && _settings.IsHeaderRequired)
            throw new InvalidOperationException("Notification Header must not be empty");
        if (notificationDto.StartTime > _timeService.CurrentTime)
            throw new InvalidOperationException("Notification StartTime must be greater then current time");
        if (notificationDto.MaxAmount < 0)
            throw new InvalidOperationException("Notificator MaxAmount can`t be less then 0");
        if (notificationDto.Interval != null || notificationDto.Interval < _settings.MinInterval)
            throw new InvalidOperationException("Notificator Interval could be empty or Interval value must be greater then MinInterval");
        if (notificationDto.EndTime != null || notificationDto.EndTime < notificationDto.StartTime && (notificationDto.StartTime - notificationDto.EndTime) < _settings.MinInterval)
            throw new InvalidOperationException("Notificator EndTime could be empty or he can`t be less then StartTime "
            + "and difference between StartTime and EndTime shouldn't be less then MinInterval");
        

    }
}
