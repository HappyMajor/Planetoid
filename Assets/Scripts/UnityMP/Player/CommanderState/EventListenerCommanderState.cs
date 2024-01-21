using Frictionless;
using System;

public abstract class EventListenerCommanderState<T> : ICommanderState
{
    private Action<T> handler;
    protected CommanderStateManager stateManager;
    public virtual void OnEnter(CommanderStateManager stateManager)
    {
        this.stateManager = stateManager;
        this.handler = EventHandler;
        MessageRouter.AddHandler(handler);
    }

    public virtual void OnExit(CommanderStateManager stateManager)
    {
        MessageRouter.RemoveHandler(handler);
    }

    public virtual void OnUpdate(CommanderStateManager stateManager){}

    public abstract void Exit();

    public void EventHandler(T ev)
    {
        this.Exit();
    }
}