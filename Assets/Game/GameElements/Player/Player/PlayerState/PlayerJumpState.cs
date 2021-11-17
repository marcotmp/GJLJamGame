using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerJumpState : PlayerState
{
    public BoxDetector groundDetector;
    private bool isOnGround;

    public override void Enter()
    {
        Debug.Log("PlayerJumpState.Enter");
        base.Enter();
        player.move.performed += OnActionMove;
        player.move.canceled += OnActionMove;

        player.jump.performed += OnActionJumpStarted;
        player.jump.canceled += OnActionJumpCancelled;

    }

    public override void Exit()
    {
        base.Exit();
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
        if (player.IsOnGround)
            fsm.ChangeState<PlayerMoveState>();
        //else

    }

    public void OnActionMove(CallbackContext c)
    {
        //Debug.Log("PlayerJumpState.OnActionMove");
        player.Move(c.ReadValue<Vector2>());
    }



    private void OnActionJumpStarted(CallbackContext c)
    {
        Debug.Log("PlayerJumpState Jump Started");
        //player.StartJump();
        //fsm.ChangeState<PlayerJumpState>();
    }

    private void OnActionJumpCancelled(CallbackContext c)
    {

    }
}

