using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerMountedState : PlayerState
{
    public override void Enter()
    {
        Debug.Log("PlayerMountedState");
        base.Enter();
        player.MountAction();
        
        player.action.performed += OnActionUnmountStarted;
    }

    public override void Exit()
    {
        base.Exit();

        player.action.performed -= OnActionUnmountStarted;
    }

    void OnActionUnmountStarted(CallbackContext c)
    {
        Debug.Log("Player unmount action pressed.");
        fsm.ChangeState<PlayerUnmountingState>();
    }
}
