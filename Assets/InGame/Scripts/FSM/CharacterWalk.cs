using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : CharacterAbility
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void onEnter()
    {
        Debug.Log("걷기 상태 돌입");
    }

    protected override void onExit()
    {

    }

    protected override void onUpdate()
    {
        //Walk playing
    }
}
