namespace Notificator.Infrastructure.Inspection;

/// <summary>
/// Объект, ответственный за определение статуса уведомлений
/// </summary>
public class NotificationInspector : INotificationInspector
{
    public bool ShouldBeCompleted(Notification notification)
    {
        if (notification == null)
            throw new ArgumentNullException();
        if (notification.EndTime == null && notification.History == null)
            return false;
        if (notification.EndTime == null && notification.History != null)
            return false;
        return false;
        
    }
}
