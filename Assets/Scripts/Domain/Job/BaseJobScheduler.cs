using System.Diagnostics;
using Planetoid.Logging;
public class BaseJobScheduler
{
    private IJobExecutor jobExecutor;
    private ITimerProvider timerProvider;
    public BaseJobScheduler(IJobExecutor executor)
    {
        this.timerProvider = new SystemTimerProvider();
        this.jobExecutor = executor;
        this.timerProvider.ElapsedTimeEvent += this.ElapsedTimeEventHandler;
        this.timerProvider.Start();
    }

    private void ElapsedTimeEventHandler(long timeMillis)
    {
        this.jobExecutor.ElapsedTime(timeMillis);
    }

    public void Schedule()
    {
        this.jobExecutor.Schedule();
    }
}