using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class HeliMove : HeliState
{
    public float maxSpeed = 5f;
    public float acceleration = 30f;
    public float decceleration = 20f;
    public float gravity = 20f;
    public float maxGravity = 8f;

    private bool isOnGround = false;

    private Vector2 moveDirection = Vector2.zero;
    private Vector2 motion = Vector2.zero;

    public HeliMove(PlayerHelicopter ph) : base(ph)
    {

    }

    public override void Enter()
    {
        playerHelicopter.move.performed += SetMovementInput;
        playerHelicopter.grab.performed += SetGrabInput;

        playerHelicopter.anim.SetBool("Move", true);
        motion = playerHelicopter.rb.velocity;
    }

    public override void FixedUpdate()
    {
        if (moveDirection != Vector2.zero)
        {
            motion.x += moveDirection.x * acceleration * Time.deltaTime;
            motion.y += moveDirection.y * acceleration * Time.deltaTime;
            motion.x = Mathf.Clamp(motion.x, -maxSpeed, maxSpeed);
        }
        else
        {
            motion.x = Mathf.MoveTowards(motion.x, 0, decceleration * Time.deltaTime);
        }

        if (!isOnGround) motion.y -= gravity * Time.deltaTime;
        motion.y = Mathf.Clamp(motion.y, -maxGravity, maxSpeed);

        bool wasOnGround = isOnGround;
        isOnGround = playerHelicopter.groundDetector.CheckCollision();
        if (!wasOnGround && isOnGround) motion.y = 0;

        playerHelicopter.rb.velocity = motion;
    }

    public override void Exit()
    {
        playerHelicopter.move.performed -= SetMovementInput;
        
        playerHelicopter.anim.SetBool("Move", false);
    }

    void SetMovementInput(CallbackContext ctx)
    {
        Vector2 inputDirection = ctx.ReadValue<Vector2>();
        moveDirection.x = inputDirection.x;
        moveDirection.y = inputDirection.y;

        Vector2.ClampMagnitude(moveDirection, 1);
    }

    void SetGrabInput(CallbackContext ctx)
    {
        if (playerHelicopter.ObjectInHand())
            playerHelicopter.Release();
        else
            playerHelicopter.Grab();
    }
}
