namespace Notificator.Core.Models;

/// <summary>
/// Уведомление
/// </summary>
public class Notification
{
    /// <summary>
    /// Заголовок уведомления 
    /// </summary>
    public string Header { get; }

    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// Время первой отправки уведомления
    /// </summary>
    public DateTimeOffset StartTime { get; }

    /// <summary>
    /// Время последней отправки уведомления
    /// </summary>
    public DateTime? EndTime { get; }

    /// <summary>
    /// Интервал отправки уведомления
    /// </summary>
    public TimeSpan? Interval { get; }

    /// <summary>
    /// Максимально кол-во отправок уведомления
    /// </summary>
    public int MaxAmount { get; }

    /// <summary>
    /// Дни недели, в которые может прийти уведомление
    /// </summary>
    public List<int> DaysOfTheWeek { get; }

    /// <summary>
    /// Выполненные уведомления
    /// </summary>
    public bool IsCompleted { get; }

    /// <summary>
    /// История отправки этого уведомления
    /// </summary>
    public List<SentNotification> History { get; }
}
