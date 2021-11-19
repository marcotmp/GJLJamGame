using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LegsDeactivate : LegsState
{
    //public override void Enter()
    //{
    //    base.Enter();
    //    legs.interactionSign.Enable();
    //}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        legs.ProcessStop();
    }

    //public override void Exit()
    //{
    //    base.Exit();
    //    legs.interactionSign.Disable();
    //}

}
