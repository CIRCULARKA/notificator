namespace Notificator.Tests.Unit.Infrastructure.Validation;

public class NotificationValidationServiceTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WhenNotificationTextIsEmpty_Throws(string notificationText)
    {
        // Arrange
        var settings = new NotificationValidationSettings();
        var service = new NotificationValidationService(settings);

        var notification = new NotificationDto
        {
            StartTime = new DateTime(2099, 01, 01),
            Header = "nevermind",
            Text = notificationText
        };

        Exception actualException = null;

        // Act
        try { service.Validation(notification); }
        catch (Exception e) { actualException = e; }

        // Assert
        Assert.NotNull(actualException);
    }
}
