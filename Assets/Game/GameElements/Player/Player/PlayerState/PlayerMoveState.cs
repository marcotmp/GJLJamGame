using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerMoveState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // player.move.performed += OnActionMove;
        // player.move.canceled += OnActionMove;
        // player.jump.performed += OnActionJumpStarted;
        player.action.performed += OnActionMountStarted;
    }

    public override void Exit()
    {
        base.Exit();
        // player.move.performed -= OnActionMove;
        // player.move.canceled -= OnActionMove;
        // player.jump.performed -= OnActionJumpStarted;
        player.action.performed -= OnActionMountStarted;
    }

    public override void FixedUpdate()
    {
        //Debug.Log("PlayerMoveState.FixedUpdate");
        base.FixedUpdate();

        if (!player.IsOnGround)
            fsm.ChangeState<PlayerJumpState>();
        else
            player.ProcessMove();

        // if moving down

        //else
    }

    // public void OnActionMove(CallbackContext c)
    // {
    //     var val = c.ReadValue<Vector2>();
    //     player.Move(val);
    // }

    // private void OnActionJumpStarted(CallbackContext c)
    // {
    //     Debug.Log("PlayerMoveState.OnActionJumpStarted");
    //     player.StartJump();
    //     fsm.ChangeState<PlayerJumpState>();
    // }

    private void OnActionMountStarted(CallbackContext c)
    {
        Debug.Log($"{c.phase}");
        
        if(player.IsCollidingVehicle())
        {
            fsm.ChangeState<PlayerMountingState>();
        }
    }
}
