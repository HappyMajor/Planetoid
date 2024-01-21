using Planetoid.Logging;
using UnityEngine;

public class UnityDebugLoggingProvider : ILoggingProvider
{
    public void Log(string msg)
    {
        Debug.Log(msg);
    }
}