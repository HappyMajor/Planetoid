public interface IJobExecutor
{
    public void Schedule();
    public void Reset();
    public void Pause();
    public void ElapsedTime(long timeMillis);
}