namespace Notificator.Infrastructure.Inspection;

/// <summary>
/// Объект, ответственный за определение статуса уведомлений
/// </summary>
public class NotificationInspector : INotificationInspector
{
    public bool ShouldBeCompleted(Notification notification)
    {
        ArgumentNullException.ThrowIfNull(notification);

        if (notification.IsOneTime && notification.History == null)
            return false;

        if (notification.IsOneTime && notification.History != null && notification.History.Any())
            return true;

        if (notification.IsInfinite)
            return false;
        return false;        
    }
}
