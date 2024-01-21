public class DefaultCommanderState : ICommanderState
{
    public void OnEnter(CommanderStateManager stateManager)
    {
        stateManager.ListenForEvents<BuildModeStartEvent>(this, (ev) =>
        {
            stateManager.NextState(new BuildModeState());
        });
    }

    public void OnExit(CommanderStateManager stateManager)
    {
    }

    public void OnUpdate(CommanderStateManager stateManager)
    {
    }
}