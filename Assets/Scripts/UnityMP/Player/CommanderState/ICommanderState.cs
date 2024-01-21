public interface ICommanderState
{
    public void OnEnter(CommanderStateManager stateManager);
    public void OnExit(CommanderStateManager stateManager);
    public void OnUpdate(CommanderStateManager stateManager);
}