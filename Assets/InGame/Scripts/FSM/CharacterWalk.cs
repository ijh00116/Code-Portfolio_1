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
        Debug.Log("걷기 상태 종료");
    }

    protected override void onUpdate()
    {
        //Walk playing
        Debug.Log("걷기 상태 진행중");
    }
}
