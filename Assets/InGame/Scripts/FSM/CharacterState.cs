using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IStateCallback
{
    Action OnEnter { get; }
    Action OnExit { get; }
    Action OnUpdate { get; }
}
public interface IStateCallbackListener
{
    IStateCallback stateCallback { get; }
}
public class StateMachine<T> : IStateCallbackListener where T : struct
{
    public bool Triggerevent;
    public GameObject target;
    public T PreviousState;
    public T CurrentState;

    public IStateCallback PreviousStatecallback;
    public IStateCallback currentStatecallback;
    public Dictionary<T, IStateCallback> stateLookup;

    CharacterStateMachineRunner StateMachineRunner;
    public StateMachine(GameObject obj,bool _istrigger)
    {
        target = obj;
        Triggerevent = _istrigger;

        if (StateMachineRunner == null)
            StateMachineRunner = obj.AddComponent<CharacterStateMachineRunner>();

        stateLookup = new Dictionary<T, IStateCallback>();
        StateMachineRunner.Initialize(this);
    }

    public IStateCallback stateCallback
    {
        get
        {
            return currentStatecallback;
        }
    }

    public void AddState(T state, IStateCallback statecallback)
    {
        stateLookup.Add(state, statecallback);
    }
    public void ChangeState(T state)
    {
        PreviousState = CurrentState;
        if (stateLookup.ContainsKey(PreviousState))
        {
            PreviousStatecallback = stateLookup[PreviousState];
            PreviousStatecallback?.OnExit?.Invoke();
        }

        if (stateLookup.ContainsKey(state))
        {
            currentStatecallback = stateLookup[state];
        }
        CurrentState = state;
        currentStatecallback?.OnEnter?.Invoke();
    }

    public bool IsCurrentState(T state)
    {
        if (stateLookup.ContainsKey(state))
        {
            return currentStatecallback == stateLookup[state];
        }
        else
            return false;

    }
}
