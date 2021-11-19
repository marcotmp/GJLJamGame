using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliState : State
{
    protected PlayerHelicopter playerHelicopter;

    public HeliState(PlayerHelicopter ph)
    {
        playerHelicopter = ph;
    }
}
