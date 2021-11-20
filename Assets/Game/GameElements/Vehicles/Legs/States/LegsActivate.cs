using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LegsActivate : LegsState
{
    public override void Enter()
    {
        base.Enter();
        legs.rb.gravityScale = 0;
        fsm.ChangeState<LegsMove>();
    }
}
