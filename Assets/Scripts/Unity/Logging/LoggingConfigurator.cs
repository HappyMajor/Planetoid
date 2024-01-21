using Planetoid.Logging;
using UnityEngine;

public class LoggingConfigurator : MonoBehaviour
{
    public LoggingConfigurator()
    {
        this.InitializeDomainLogger();
    }

    public void Awake()
    {
        this.InitializeDomainLogger();
    }

    private void InitializeDomainLogger()
    {
        Planetoid.Logging.Logger logger = Planetoid.Logging.Logger.Instance();
        logger.AttachLoggingProvider(new UnityDebugLoggingProvider());
    }
}