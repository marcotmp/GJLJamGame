using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerJumpState : PlayerState
{
    public BoxDetector groundDetector;
    private bool isOnGround;

    public override void Enter()
    {
        base.Enter();
        // player.move.performed += OnActionMove;
        // player.move.canceled += OnActionMove;

        // player.jump.canceled += OnActionJumpCancelled;
    }

    public override void Exit()
    {
        base.Exit();
        // player.move.performed -= OnActionMove;
        // player.move.canceled -= OnActionMove;

        // player.jump.canceled -= OnActionJumpCancelled;

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (player.IsOnGround)
            fsm.ChangeState<PlayerMoveState>();
        // move horizontally
        else
            player.ProcessMove();

        // if moving down
        //else

    }

    // public void OnActionMove(CallbackContext c)
    // {
    //     player.Move(c.ReadValue<Vector2>());
    // }

    // private void OnActionJumpCancelled(CallbackContext c)
    // {
    //     player.CancelJump();
    // }
}

