using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliActivate : HeliState
{
    public HeliActivate(PlayerHelicopter ph) : base(ph) { }

    public override void Enter()
    {
        playerHelicopter.rb.gravityScale = 0;
        fsm.ChangeState<HeliMove>();
    }
}
