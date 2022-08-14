using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterAbility
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void onEnter()
    {
        Debug.Log("Idle상태 진입");
    }

    protected override void onExit()
    {

    }

    protected override void onUpdate()
    {
        //Idle playing
    }
}
