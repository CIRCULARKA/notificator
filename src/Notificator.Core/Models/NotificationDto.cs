namespace Notificator.Core.Models;

/// <summary>
/// Model for comunication with external systems
/// </summary>
public class NotificationDto
{
    /// <summary>
    /// Заголовок уведомления 
    /// </summary>
    public string Header { get; set; }

    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Время первой отправки уведомления
    /// </summary>
    public DateTimeOffset StartTime { get; set; }

    /// <summary>
    /// Время последней отправки уведомления
    /// </summary>
    public DateTimeOffset? EndTime { get; set; }

    /// <summary>
    /// Интервал отправки уведомления
    /// </summary>
    public TimeSpan? Interval { get; set; }

    /// <summary>
    /// Максимально кол-во отправок уведомления
    /// </summary>
    public int? MaxAmount { get; set; }

    /// <summary>
    /// Дни недели, в которые может прийти уведомление
    /// </summary>
    public List<int> DaysOfTheWeek { get; set; }
}
