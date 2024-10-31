namespace Notificator.Core.Models;

/// <summary>
/// Описывает уведомление, которое было отправлено
/// </summary>
public class SentNotification
{
    /// <summary>
    /// Когда уведомление было отправлено
    /// </summary>
    public DateTimeOffset TimeSent { get; }

    /// <summary>
    /// Уведомление, которое было отправлено
    /// </summary>
    public Notification Notification { get; }

    /// <summary>
    /// Статус отправки уведомления
    /// </summary>
    public DeliveryStatus Status { get; }

    /// <summary>
    /// Подробности ошибки, которая произошла во время отправки уведомления
    /// </summary>
    public string ErrorMessage { get; }
}
