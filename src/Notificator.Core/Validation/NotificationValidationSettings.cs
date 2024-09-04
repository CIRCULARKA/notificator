namespace Notificator.Core.Validation;

/// <summary>
/// Settings for notification validation
/// </summary>
public class NotificationValidationSettings
{
    /// <summary>
    /// Should header be included in notification
    /// </summary>
    public bool IsHeaderRequired { get; set; }

    /// <summary>
    /// Minimal interval between recurring notifications
    /// </summary>
    public TimeSpan MinInterval { get; set; }
}
