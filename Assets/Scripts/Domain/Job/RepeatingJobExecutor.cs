using System;

public class RepeatingJobExecutor : IJobExecutor
{
    private long elapsedMillis;
    private Action action;
    private long intervalMillis;
    private long amountOfRepititions = -1;
    private long elapsedRepititions = 0;
    private bool isScheduled = false;
    public RepeatingJobExecutor(Action action, long intervalMillis, long amountOfRepititions)
    {
        this.action = action;
        this.intervalMillis = intervalMillis;
        this.amountOfRepititions = amountOfRepititions;
    }
    public void ElapsedTime(long timeMillis)
    {
        if(this.isScheduled)
        {
            this.elapsedMillis += timeMillis;

            if(elapsedMillis >= intervalMillis)
            {
                this.elapsedRepititions++;
                this.elapsedMillis = 0;
                this.action?.Invoke();
            }

            if(elapsedRepititions >= this.amountOfRepititions)
            {
                this.Pause();
            }
        }
    }

    public void Pause()
    {
        this.isScheduled = false;
    }

    public void Reset()
    {
        this.elapsedMillis = 0;
        this.elapsedRepititions = 0;
    }

    public void Schedule()
    {
        this.isScheduled = true;
    }
}