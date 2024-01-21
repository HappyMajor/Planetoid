using System.Timers;

public class SystemTimerProvider : ITimerProvider
{
    private long INTERVAL = 1;
    private long elapsedMillis;
    public long ElapsedMillis { get => elapsedMillis; set => elapsedMillis = value; }

    public event ITimerProvider.ElapsedTimeEventHandler ElapsedTimeEvent;

    public void Start()
    {
        this.ElapsedTimeEvent += this.ElapsedTimeEventHandler;
        Timer timer = new Timer(INTERVAL); //1 second interval
        timer.AutoReset = true;
        timer.Enabled = true;
        timer.Elapsed += (source, time) => ElapsedTimeEvent?.Invoke(INTERVAL);
        timer.Start();
    }

    public void ElapsedTimeEventHandler(long timeMillis)
    {
        this.elapsedMillis += timeMillis;
    }
}