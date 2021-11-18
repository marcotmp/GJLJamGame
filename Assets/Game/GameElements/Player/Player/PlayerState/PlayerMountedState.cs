using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerMountedState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        
        player.action.performed += OnActionUnmountStarted;
    }

    public override void Exit()
    {
        base.Exit();

        player.action.performed -= OnActionUnmountStarted;
    }

    void OnActionUnmountStarted(CallbackContext c)
    {
        fsm.ChangeState<PlayerUnmountingState>();
    }
}
