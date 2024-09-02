namespace Notificator.Core.Logging.Interfaces.ILogsEnricher;

public interface ILogsEnricher
{
    public string Enrich(string logMessage);
}
