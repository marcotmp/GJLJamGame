using UnityEngine;

[System.Serializable]
public class JumpHandler
{

    [SerializeField] private float upForce = 30;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 20;
    public bool isJumping { get; private set; } = false;

    private float _gravityScale = 0;

    internal Vector2 velocity;
    internal Rigidbody2D rigidbody;


    // called when the player press the jump button
    internal void StartJump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.gravityScale = gravityScale;
        _gravityScale = gravityScale;
        rigidbody.AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
        isJumping = true;
    }

    // called when player release the jump button
    internal void CancelJumpImpulse()
    {
        rigidbody.gravityScale = fallingGravityScale;
        _gravityScale = fallingGravityScale;
    }

    // called when player touches ground
    internal void CancelJump()
    {
        isJumping = false;
    }

    // called inside fixed update
    internal void Update()
    {
        this.velocity = rigidbody.velocity;

        if (rigidbody.velocity.y < 0 && rigidbody.gravityScale < fallingGravityScale)
            rigidbody.gravityScale = fallingGravityScale;

        //if (rigidbody.velocity.y < 0 && _gravityScale < fallingGravityScale)
        //    _gravityScale = fallingGravityScale;

        // move horizontally
        //rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y);
    }
}