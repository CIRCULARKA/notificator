namespace Notificator.Core.Common;

class TimeService : ITimeService
{
    public DateTimeOffset CurrentTime => DateTimeOffset.Now;
}