using Notificator.Core.Logging.Interfaces.ILogsEnricher;

namespace Notificator.Core.Logging.Implementation.EnvironmentLogsEnricher;

public class EnvironmentLogsEnricher : ILogsEnricher
{
    string _environment;
    public EnvironmentLogsEnricher(string environment)
    {
        _environment = environment;
    }

    public string Enrich(string logMessage) 
    {
        return $"[{_environment}]" + logMessage;
    }
    
}
