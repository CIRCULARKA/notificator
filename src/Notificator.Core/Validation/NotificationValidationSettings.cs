namespace Notificator.Core.Validation;

public class NotificationValidationSettings
{
    public bool IsHeaderRequired { get; set; }

    public TimeSpan MinInterval { get; set; }
}