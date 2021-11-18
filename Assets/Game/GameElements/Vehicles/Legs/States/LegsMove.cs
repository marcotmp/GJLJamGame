using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class LegsMove : LegsState
{

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!legs.IsOnGround)
            fsm.ChangeState<LegsAir>();
        else
            legs.ProcessMove();
    }

    public override void Enter()
    {
        base.Enter();
        legs.move.performed += OnLegsMove;
        legs.jump.performed += OnLegsJump;
    }

    public override void Exit()
    {
        base.Exit();
        legs.move.performed -= OnLegsMove;
        legs.jump.performed -= OnLegsJump;
    }

    void OnLegsMove(CallbackContext ctx)
    {
        legs.Move(ctx.ReadValue<Vector2>());
    }

    void OnLegsJump(CallbackContext ctx)
    {
        legs.StartJump();
        fsm.ChangeState<LegsAir>();
    }
}
