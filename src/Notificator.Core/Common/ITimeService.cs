namespace Notificator.Core.Common;

/// <summary>
/// Time abstraction
/// </summary>
public interface ITimeService
{
    /// <summary>
    /// Gets current time
    /// </summary>
    public DateTimeOffset CurrentTime { get; }
}