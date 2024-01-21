using System;

public class SystemOfflineException : Exception
{
    public SystemOfflineException() { }
    public SystemOfflineException(string message) : base(message) { }
}