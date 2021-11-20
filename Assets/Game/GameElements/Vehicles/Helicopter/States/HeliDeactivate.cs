using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliDeactivate : HeliState
{
    public float decceleration = 20f;
    public float gravity = 20f;
    public float maxGravity = 8f;
    
    private Vector2 motion = Vector2.zero;
    private bool isOnGround = false;

    public HeliDeactivate(PlayerHelicopter ph) : base(ph)
    {
        
    }
    
    public override void Enter()
    {
        // motion = playerHelicopter.rb.velocity;
        motion = Vector3.zero;
        playerHelicopter.rb.gravityScale = 1;
    }

    public override void FixedUpdate() 
    {
        // motion.x = Mathf.MoveTowards(motion.x, 0, decceleration * Time.deltaTime);

        // if (!isOnGround) motion.y -= gravity * Time.deltaTime;
        // motion.y = Mathf.Max(motion.y, -maxGravity);

        // bool wasOnGround = isOnGround;
        // isOnGround = playerHelicopter.groundDetector.CheckCollision();
        // if (!wasOnGround && isOnGround) motion.y = 0;

        // playerHelicopter.rb.velocity = motion;
    }
}
