public interface ITimerProvider
{
    public void Start();
    public long ElapsedMillis { get; set; }
    public delegate void ElapsedTimeEventHandler(long timeMillis);
    public event ElapsedTimeEventHandler ElapsedTimeEvent;
}