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
        return false;
        
    }
}
