using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    public float delay;
    private Type nextState;
    private float timer = 0;

    public WaitState(float delay, Type nextState)
    {
        this.delay = delay;
        this.nextState = nextState;
    }

    public override void Enter()
    {
        base.Enter();

        timer = 0;
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            timer = 0;
            fsm.ChangeState(nextState);
        }
    }
}
