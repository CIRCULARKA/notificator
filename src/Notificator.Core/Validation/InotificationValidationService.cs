namespace Notificator.Core.Validation;

/// <summary>
/// Interface for validation service
/// </summary>
public interface INotificationValidationService
{
    /// <summary>
    /// Validates notification
    /// </summary>
    public void Validation(NotificationDto notificationDto);
}
