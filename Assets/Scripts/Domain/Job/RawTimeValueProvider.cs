public class RawTimeValueProvider : ITimerProvider
{
    public long ElapsedMillis {  get; set; }

    public event ITimerProvider.ElapsedTimeEventHandler ElapsedTimeEvent;
    public void Start()
    {
    }

    public void Provide(long deltaTimeMillis)
    {
        this.ElapsedTimeEvent?.Invoke(deltaTimeMillis);
    }
}