namespace Notificator.Tests.Unit.Infrastructure.Inspection;

public class NotificationInspectorTests
{
    [Fact]
    public void ShouldBeCompleted_WhenNotificationIsNull_Throws()
    {
        // Arrange
        var inspector = CreateInspector();

        // Act && Assert
        Assert.Throws<ArgumentNullException>(() => inspector.ShouldBeCompleted(null));
    }

    [Fact]
    public void ShouldBeCompleted_OneTimeNotificationEmptyHistory_False()
    {
        // Arrange
        var inspector = CreateInspector();

        var oneTimeNotification = CreateValidNotification();

        // Act && Arrange
        Assert.False(inspector.ShouldBeCompleted(oneTimeNotification));
    }

    [Fact]
    public void ShouldBeCompleted_OneTimeNotificationHasHistoryRecord_True()
    {
        // Arrange
        var inspector = CreateInspector();

        var oneTimeNotification = CreateValidNotification();

        // Act && Arrange
        Assert.False(inspector.ShouldBeCompleted(oneTimeNotification));
    }

    /// <summary>
    /// Создаёт уведомление. Подразумевается, что уведомление корректно.
    /// По умолчанию создаётся одноразовое уведомление
    /// </summary>
    /// <param cref="startTime">Время первой отправки уведомления. Если не указано, то равно одному тику (UTC)</param>
    /// <param cref="endTime">Максимальное время отправки уведомления. Опционально</param>
    /// <param cref="interval">Интервал отправки уведомления. Опционально</param>
    /// <param cref="maxAmount">Максимальное кол-во отправок уведомления. Опционально</param>
    /// <param cref="daysOfTheWeek">Дни недели, в которые уведомление может быть отправлено. По умолчанию - все дни недели</param>
    private Notification CreateValidNotification(
        DateTimeOffset? startTime = null, 
        DateTimeOffset? endTime = null,
        TimeSpan? interval = null,
        int maxAmount = 1,
        List<int> daysOfTheWeek = null)
    {
        return new Notification
        {
            Header = $"Notification' header ({Guid.NewGuid()})",
            Text = $"Notification' text ({Guid.NewGuid()})",
            StartTime = startTime ?? new DateTimeOffset(ticks: 1, TimeSpan.FromHours(0)),
            EndTime = endTime,
            Interval = interval,
            MaxAmount = maxAmount,
            DaysOfTheWeek = daysOfTheWeek ?? new List<int> { 1, 2, 3, 4, 5, 6, 7 }
        };
    }

    private NotificationInspector CreateInspector()
    {
        return new NotificationInspector();
    }
}
