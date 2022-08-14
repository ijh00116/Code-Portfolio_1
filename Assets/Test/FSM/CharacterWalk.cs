using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : CharacterAbility
{
    Animator animator;

    protected override void Start()
    {
        base.Start();
    }
    protected override void onEnter()
    {
        animator.Play("Walk");
    }

    protected override void onExit()
    {

    }

    protected override void onUpdate()
    {
        //Walk playing
    }
}
