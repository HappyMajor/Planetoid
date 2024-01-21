using Planetoid.Logging;
using UnityEngine;

public class LogProvider : ILoggingProvider
{
    public void Log(string msg)
    {
        Debug.Log(msg);
    }
}