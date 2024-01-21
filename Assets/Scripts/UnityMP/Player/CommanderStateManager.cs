using Frictionless;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CommanderStateManager : MonoBehaviour
{
    [SerializeField]
    private ICommanderState currentState;

    private Action unregisterHandlersOfCurrentState;
    public void Start()
    {
        this.NextState(new DefaultCommanderState());
    }
    public void ListenForEvents<EVENT>(ICommanderState state, Action<EVENT> handler)
    {
        CommanderStateEventListener<ICommanderState,EVENT> eventManager = new CommanderStateEventListener<ICommanderState, EVENT> ();
        eventManager.RegisterHandlerForState (state, handler);
        unregisterHandlersOfCurrentState = () => eventManager.RemoveHandlersOfState (state);
    }

    public void NextState(ICommanderState state)
    {
        if(currentState != null)
        {
            this.currentState.OnExit(this);
            this.unregisterHandlersOfCurrentState();
            this.unregisterHandlersOfCurrentState = null;
        }
        this.currentState = state;
        this.currentState.OnEnter(this);
    }


    public void Update()
    {
        if(currentState != null)
        {
            this.currentState.OnUpdate(this);
        }
    }
}

public enum CommanderState
{
    BUILD_MODE, DEFAULT
}