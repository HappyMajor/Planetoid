using System;
using System.Diagnostics;
using Planetoid.Logging;

public class DoLaterJobExecutor : IJobExecutor
{
    private long elapsedTimeMillis = 0;
    private bool isScheduled = false;
    private Action action;
    private long delay = 0;

    public DoLaterJobExecutor(Action action, long delay)
    {
        this.action = action;
        this.delay = delay;
    }

    public void ElapsedTime(long timeMillis)
    {
        if (this.isScheduled)
        {
            this.elapsedTimeMillis += timeMillis;
            if(elapsedTimeMillis >= this.delay)
            {
                this.Pause();
                this.action?.Invoke();
            }
        } 
    }

    public void Pause()
    {
        this.isScheduled = false;
    }

    public void Reset()
    {
        this.elapsedTimeMillis = 0;
    }

    public void Schedule()
    {
        this.isScheduled = true;
    }
}