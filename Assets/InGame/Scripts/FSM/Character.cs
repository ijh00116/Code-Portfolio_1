using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public StateMachine<eActorState> _state;
    
    protected virtual void Awake()
    {
        _state = new StateMachine<eActorState>(this.gameObject, true);
    }
}
