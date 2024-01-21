public interface ICelestialWalkerState
{
    public void OnEnter(CelestialWalker walker);
    public void OnUpdate(CelestialWalker walker);
    public void OnExit(CelestialWalker walker);
}