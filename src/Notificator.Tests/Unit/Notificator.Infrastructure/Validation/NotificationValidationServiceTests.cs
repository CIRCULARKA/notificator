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
        notification.Interval = notificationInterval;

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

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_WhenMaxAmountLessOrEqualToZero_Throws(int maxAmount)
    {
        // Arrange
        var service = CreateValidationService();

        var notification = CreateValidNotification();
        notification.MaxAmount = maxAmount;

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
    } 

    [Theory]
    [MemberData(nameof(DaysOfTheWeekWithDaysGreaterThan7AndLessThan1))]
    public void Validate_WhenDaysOfTheWeekIsLessThan1OrGreaterThan7_Thorws(IList<int> daysOfTheWeek)
    {
        // Arrange
        var service = CreateValidationService();

        var notification = CreateValidNotification();
        notification.DaysOfTheWeek = daysOfTheWeek.ToList();

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
    }

    [Theory]
    [MemberData(nameof(DaysOfTheWeekWithDuplicateValues))]
    public void Validate_WhenDaysOfTheWeekHasDuplicates_Thorws(IList<int> daysOfTheWeek)
    {
        // Arrange
        var service = CreateValidationService();

        var notification = CreateValidNotification();
        notification.DaysOfTheWeek = daysOfTheWeek.ToList();

        // Act
        var actualException = GetExceptionOfNotificationValidation(service, notification);

        // Assert
        Assert.NotNull(actualException);
    } 

    public static IEnumerable<object[]> DaysOfTheWeekWithDaysGreaterThan7AndLessThan1 = new List<object[]>
    {
        new object[] { new List<int> { 0, 2, 3, 4, 1 } },
        new object[] { new List<int> { 1, 2, 3, 7, 8 } },
        new object[] { new List<int> { 1, 2, -1, 7, 4 } }
    };

    public static IEnumerable<object[]> DaysOfTheWeekWithDuplicateValues = new List<object[]>
    {
        new object[] { new List<int> { 1, 1, 2, 3, 4 } },
        new object[] { new List<int> { 1, 2, 3, 3, 4, 5 } },
        new object[] { new List<int> { 1, 2, 3, 4, 5, 5, 7, 7 } }
    };

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
    /// text, header, interval is emtpy,
    /// start time equals to 01/01/2024, end time is emtpy
    /// max amount equals to zero, days of the week - all
    /// </summary>
    private NotificationDto CreateValidNotification() =>
        new NotificationDto
        {
            Header = "The Header",
            Text = "The text",
            StartTime = DateTimeOffset.ParseExact("01/01/2024", "MM/dd/yyyy", formatProvider: null),
            EndTime = null,
            MaxAmount = 10,
            DaysOfTheWeek = new List<int> { 1, 2, 3, 4, 5, 6, 7 }
        };

    /// <summary>
    /// Creates notification validation service from it's dependencies.
    /// Fakes are inserted as dependencies if they are <see langword="null" />
    /// If <paramref name="timeService"/> is <see langword="null" /> then the
    /// <see cref="ITimeService.CurrentTime" /> will return <see cref="DateTimeOffset.MaxValue" />
    /// </summary>
    public NotificationValidationService CreateValidationService(
        ITimeService timeService = null,
        NotificationValidationSettings settings = null)
    {
        if (timeService is null)
        {
            var timeServiceMock = new Mock<ITimeService>();
            timeServiceMock.Setup(s => s.CurrentTime).Returns(DateTimeOffset.MaxValue);
            timeService = timeServiceMock.Object;
        }

        if (settings is null)
            settings = new Mock<NotificationValidationSettings>().Object;

        return new NotificationValidationService(settings, timeService);
    }
}
