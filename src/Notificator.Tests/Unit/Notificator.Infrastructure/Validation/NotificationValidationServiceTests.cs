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
        var service = CreateValidationService();

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
    public void Validate_WhenHeaderIsEmptyAndHeaderIsRequired_Throws(string notificationHeader)
    {
        // Arrange
        var settings = new NotificationValidationSettings();
        settings.IsHeaderRequired = true;

        var service = CreateValidationService(settings: settings);

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
    public void Validate_WhenIntervalLessThenSettingsMinInterval_Throws()
    {
        // Arrange
        var interval = TimeSpan.FromMinutes(10);

        var settings = new Mock<NotificationValidationSettings>();
        settings.Setup(s => s.MinInterval).Returns(TimeSpan.FromMinutes(30));
        var service = CreateValidationService(settings: settings.Object);

        var notification = new NotificationDto
        {
            Text = "whatever",
            Header = "whatever",
            StartTime = DateTimeOffset.MaxValue,
            Interval = interval
        };

        Exception actualExeption = null;

        // Act
        try { service.Validation(notification); }
        catch (Exception e) { actualExeption = e; }

        // Assert
        Assert.NotNull(actualExeption);
    }

    [Fact]
    public void Validate_WhenNotificationStartTimeIsNotEmptyAndLaterThenDateTimeNow_Throws()
    {
        // Arrange
        var currentTime = new DateTimeOffset(ticks: 10000, TimeSpan.FromHours(0));
        var notificationStartTime = new DateTimeOffset(ticks: 11000, TimeSpan.FromHours(0));

        var timeService = new Mock<ITimeService>();
        timeService.SetupGet(t => t.CurrentTime).Returns(currentTime);

        var service = CreateValidationService(timeService: timeService.Object);

        var notification = new NotificationDto
        {
            Text = "whatever",
            Header = "whatever",
            StartTime = notificationStartTime
        };

        Exception actualExeption = null;

        // Act
        try { service.Validation(notification); }
        catch (Exception e) { actualExeption = e; }

        // Assert
        Assert.NotNull(actualExeption);
    } 

    public NotificationValidationService CreateValidationService(
        ITimeService timeService = null,
        NotificationValidationSettings settings = null
    )
    {
        if (timeService is null)
            timeService = new Mock<ITimeService>().Object;

        if (settings is null)
            settings = new Mock<NotificationValidationSettings>().Object;

        return new NotificationValidationService(settings, timeService);
    }
}
