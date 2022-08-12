using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eActorState
{
    Idle,
    Move,

    End
}
public class CharacterAbility : MonoBehaviour,IStateCallback
{
    [SerializeField] protected eActorState Mystate;

    protected Character _character = null;
    protected StateMachine<eActorState> _State;

    public Action OnEnter => onEnter;

    public Action OnExit => onExit;

    public Action OnUpdate => onUpdate;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _character = GetComponent<Character>();
        _State = _character._state;

        _State.AddState(Mystate, this);
    }

    protected virtual void onEnter()
    {

    }
    protected virtual void onExit()
    {

    }
    protected virtual void onUpdate()
    {

    }

}
