namespace Notificator.Core.Models;

public class Notification
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid ID { get; set; } 

    /// <summary>
    /// Уникальный идентификатор пользователя, владеющего уведомлением
    /// </summary>
    public Guid OwnerID { get; set; }

    /// <summary>
    /// Текст уведомления
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Время первой отправки уведомления
    /// </summary>
    public DateTimeOffset StartTime { get; set; }

    /// <summary>
    /// Интервал отправки уведомления
    /// </summary>
    public TimeSpan? Interval { get; set; }

    /// <summary>
    /// Максимально кол-во отправок уведомления
    /// </summary>
    public int MaxAmount { get; set; }

    /// <summary>
    /// Дни недели, в которые может прийти уведомление
    /// </summary>
    public List<int> DaysOfTheWeek { get; set; }
}