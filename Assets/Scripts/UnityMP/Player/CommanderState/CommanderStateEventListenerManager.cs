using Frictionless;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CommanderStateEventListener<STATE, EVENT> where STATE : ICommanderState
{
    private readonly Dictionary<ICommanderState, List<Action<EVENT>>> registeredHandlers = new();
    public void RegisterHandlerForState(ICommanderState state, Action<EVENT> handler)
    {
        MessageRouter.AddHandler<EVENT>(handler);

        if(registeredHandlers.ContainsKey(state))
        {
            List<Action<EVENT>> handlers = registeredHandlers[state];
            handlers.Add(handler);
        } else
        {
            registeredHandlers[state] = new List<Action<EVENT>>(){handler};
        }
    }

    public bool HasRegisteredHandlers(ICommanderState state)
    {
        return registeredHandlers.ContainsKey(state);
    }

    public void RemoveHandlersOfState(ICommanderState state)
    {
        if(registeredHandlers.ContainsKey(state))
        {
            List<Action<EVENT>> handlers = registeredHandlers[state];
            foreach(var handler in handlers)
            {
                MessageRouter.RemoveHandler<EVENT>(handler);
            }
            registeredHandlers.Remove(state);
        }
    }
}