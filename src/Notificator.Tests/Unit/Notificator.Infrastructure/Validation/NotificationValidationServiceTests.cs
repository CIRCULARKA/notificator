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

        var notification = CreateValidNotification();
        notification.Text = notificationText;

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

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

        var notification = CreateValidNotification();
        notification.Header = notificationHeader;

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
    }

    [Fact]
    public void Validate_WhenIntervalLessThenSettingsMinInterval_Throws()
    {
        // Arrange
        var notificationInterval = TimeSpan.FromMinutes(10);

        var settings = new Mock<NotificationValidationSettings>();
        settings.Setup(s => s.MinInterval).Returns(notificationInterval - TimeSpan.FromMinutes(5));

        var service = CreateValidationService(settings: settings.Object);

        var notification = CreateValidNotification();

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
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

        var notification = CreateValidNotification();

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
    } 

    /// <summary>
    /// Returns an exception that could be thrown while
    /// executing <see cref="NotificationValidationService.Validate(NotificationDto)"/>
    /// </summary>
    private Exception GetExceptionOfNotificationValidation(NotificationValidationService service, NotificationDto dto)
    {
        Exception result = null;

        try { service.Validate(dto); }
        catch (Exception e) { result = e; }

        return result;
    }

    /// <summary>
    /// Creates notification's DTO that has:
    /// text, header, interval equals to 5 minutes,
    /// start time equals to 01/01/2024, end time is emtpy
    /// max amount equals to zero, days of the week - all
    /// </summary>
    private NotificationDto CreateValidNotification() =>
        new NotificationDto
        {
            Header = "The Header",
            Text = "The text",
            Interval = TimeSpan.FromMinutes(5),
            StartTime = DateTimeOffset.ParseExact("01/01/2024", "MM/dd/yyyy", formatProvider: null),
            EndTime = null,
            MaxAmount = 10,
            DaysOfTheWeek = new List<int> { 1, 2, 3, 4, 5, 6, 7 }
        };

    /// <summary>
    /// Creates notification validation service from it's dependencies.
    /// Fackes are inserted as dependencies if they are null
    /// </summary>
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
