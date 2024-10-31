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

    private NotificationInspector CreateInspector()
    {
        return new NotificationInspector();
    }
}
