using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterAbility
{
    protected override void Start()
    {
        base.Start();
        _character._state.ChangeState(Mystate);
    }
    protected override void onEnter()
    {
        Debug.Log("Idle상태 진입");
    }

    protected override void onExit()
    {
        Debug.Log("Idle상태 종료");
    }

    protected override void onUpdate()
    {
        //Idle playing
        Debug.Log("Idle상태 진행중");
    }
}
