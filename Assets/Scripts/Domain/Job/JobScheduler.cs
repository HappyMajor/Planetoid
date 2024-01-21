using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Timers;
public class JobScheduler
{
    private Dictionary<Timer, RepeatingJobHandler> timers = new Dictionary<Timer, RepeatingJobHandler>();
    public delegate void RepeatingJobHandler(long elapsedTime);

    public void ScheduleRepeatingJob(RepeatingJobHandler repeatingJobHandler, long timeMillisk)
    {
        Timer timer = new System.Timers.Timer(2000);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = true;
        timer.Enabled = true;
        timers.Add(timer, repeatingJobHandler);
    }
    
    private void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Timer timer = (Timer)source;
        if(timers.ContainsKey(timer))
        {
            timers[timer]?.Invoke(e.SignalTime.Ticks/1000000);
        } else
        {
            throw new System.Exception("Timer not found");
        }
    }
}