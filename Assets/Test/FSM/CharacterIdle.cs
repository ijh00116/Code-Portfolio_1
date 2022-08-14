using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterAbility
{
    Animator animator;

    protected override void Start()
    {
        base.Start();
    }
    protected override void onEnter()
    {
        animator.Play("Idle");
    }

    protected override void onExit()
    {

    }

    protected override void onUpdate()
    {
        //Idle playing
    }
}
