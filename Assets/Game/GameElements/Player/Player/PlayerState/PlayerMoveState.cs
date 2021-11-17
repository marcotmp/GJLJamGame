using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerMoveState : PlayerState
{
    public override void Enter()
    {
        Debug.Log("PlayerMoveState.Enter");
        base.Enter();
        //player.RegisterMoveControl();
        player.move.performed += OnActionMove;
        player.move.canceled += OnActionMove;
        player.jump.performed += OnActionJumpStarted;
        player.jump.canceled += OnActionJumpCancelled;
    }

    public override void Exit()
    {
        base.Exit();
        //player.UnregisterMoveControl();
        player.move.performed -= OnActionMove;
        player.move.canceled -= OnActionMove;
        player.jump.performed -= OnActionJumpStarted;
        player.jump.canceled -= OnActionJumpCancelled;
    }

    public override void FixedUpdate()
    {
        //Debug.Log("PlayerMoveState.FixedUpdate");
        base.FixedUpdate();

        // move horizontally
        player.ProcessMove();

        // if moving down
        if (!player.IsOnGround)
            fsm.ChangeState<PlayerJumpState>();

        //else
    }

    public void OnActionMove(CallbackContext c)
    {
        var val = c.ReadValue<Vector2>();
        //Debug.Log($"PlayerMoveState.OnActionMove {val}");
        player.Move(val);
    }

    private void OnActionJumpStarted(CallbackContext c)
    {
        Debug.Log("PlayerMoveState.OnActionJumpStarted");
        player.StartJump();
        fsm.ChangeState<PlayerJumpState>();
    }

    private void OnActionJumpCancelled(CallbackContext c)
    {
        player.CancelJump();
    }

}
