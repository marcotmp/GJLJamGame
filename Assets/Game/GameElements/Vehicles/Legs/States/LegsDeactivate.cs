using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LegsDeactivate : LegsState
{
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        legs.ProcessStop();
    }
}
