namespace Notificator.Core.Interfaces.Inspection;

/// <summary>
/// Интерфейс для объекта, ответственного за определение статуса уведомлений
/// </summary>
public interface INotificationInspector
{
    /// <summary>
    /// Определяет, является ли уведомление "исчерпанным", т.е. нужно ли будет его
    /// отправлять в следующий раз
    /// </summary>
    /// <param name="notification">Уведомление, которое нужно проверить</param>
    public bool ShouldBeCompleted(Notification notification);
}
