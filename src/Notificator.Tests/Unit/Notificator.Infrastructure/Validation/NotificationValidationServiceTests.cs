using Microsoft.VisualBasic;
using Xunit.Sdk;

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

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void Validate_WhenNotificationHeaderIsRequired_Throws(string notificationHeader)
    {
        // Arrange
        var settings = new NotificationValidationSettings();
        settings.IsHeaderRequired = true;
        var service = new NotificationValidationService(settings);


        var notification = new NotificationDto
        {
            Text = "hghgh",
            Header = notificationHeader
        };

        Exception actualeExeption = null;

        // Act
        try { service.Validation(notification); }
        catch (Exception e) { actualeExeption = e; }

        // Assert
        Assert.NotNull(actualeExeption);
    }  

    [Fact]
    public void Validate_WhenNotificationStartTimeIsNotEmptyAndLaterThenDateTimeNow_Throws()
    {
        // Arrange
        var settings = new NotificationValidationSettings();
        
        var service = new NotificationValidationService(settings);

        var notification = new NotificationDto
        {
            Text = "whatever",
            Header = "whatever",
            StartTime = new DateTimeOffset(2099, 09, 04, 0, 0, 0, TimeSpan.FromDays(0))
        };

        Exception actualExeption = null;

        // Act
        try { service.Validation(notification); }
        catch (Exception e) { actualExeption = e; }

        // Assert
        Assert.NotNull(actualExeption);
    } 
}
