using System;

public interface IJobExecutionEnvironment
{
    public void AddJob(Action action);
    public void RemoveJob(Action action);
}