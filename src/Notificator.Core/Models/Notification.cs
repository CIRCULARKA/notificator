namespace Notificator.Core.Models;

/// <summary>
/// Уведомление
/// </summary>
public class Notification
{
    /// <summary>
    /// Заголовок уведомления 
    /// </summary>
    public string Header { get; init; }

    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string Text { get; init; }

    /// <summary>
    /// Время первой отправки уведомления
    /// </summary>
    public DateTimeOffset StartTime { get; init; }

    /// <summary>
    /// Время последней отправки уведомления
    /// </summary>
    public DateTimeOffset? EndTime { get; init; }

    /// <summary>
    /// Интервал отправки уведомления
    /// </summary>
    public TimeSpan? Interval { get; init; }

    /// <summary>
    /// Максимально кол-во отправок уведомления
    /// </summary>
    public int MaxAmount { get; init; }

    /// <summary>
    /// Дни недели, в которые может прийти уведомление
    /// </summary>
    public List<int> DaysOfTheWeek { get; init; }

    /// <summary>
    /// История отправки этого уведомления
    /// </summary>
    public List<SentNotification> History { get; init; }

    /// <summary>
    /// Является ли уведомление одноразовым?
    /// </summary>
    public bool IsOneTime =>
        Interval is null;
}
