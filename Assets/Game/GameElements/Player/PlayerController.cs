using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputAction move;
    private InputAction jump;
    private InputAction action;

    [SerializeField] private JumpHandler jumpHandler;
    [SerializeField] private BoxDetector groundDetector;

    [Header("Speed")]
    [SerializeField] private float horizontalSpeed;

    [Header("Testing")]
    [SerializeField] private float upForce = 30;
    [SerializeField] private float gravityScale = 10;
    [SerializeField] private float fallingGravityScale = 20;
    [SerializeField] private string moveId = "Move";


    private Vector2 dpadDir;
    //[SerializeField] private bool isJumping = false;
    private bool isOnGround;

    private bool facingRight = false;


    private void Start()
    {
        jumpHandler.rigidbody = rigidbody;

        var actionMap = inputActionAsset.FindActionMap("Gameplay");
        move = actionMap.FindAction(moveId);
        jump = actionMap.FindAction("Jump");
        action = actionMap.FindAction("Action");

        actionMap.Enable();

        //move.started += OnActionMove;
        move.performed += OnActionMove;
        move.canceled += OnActionMove;

        jump.started += OnActionJumpStarted;
        jump.canceled += OnActionJumpCancelled;

        action.performed += OnActionAction;
    }

    private void OnDestroy()
    {
        move.performed -= OnActionMove;
        move.canceled -= OnActionMove;
        jump.started -= OnActionJumpStarted;
        jump.canceled -= OnActionJumpCancelled;
        action.performed -= OnActionAction;
    }

    private void FixedUpdate()
    {
        // move vertically
        jumpHandler.Update();

        // move horizontally
        rigidbody.velocity = new Vector2(horizontalSpeed * dpadDir.x, rigidbody.velocity.y);

        // if moving down
        if (rigidbody.velocity.y <= 0)
        {
            var wasOnGround = isOnGround;
            isOnGround = groundDetector.CheckCollision();

            if (isOnGround)
            {
                //if (!wasOnGround)
                //    StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
                jumpHandler.CancelJump();
            }
        }
    }

    private void OnActionMove(CallbackContext c)
    {
        dpadDir = c.ReadValue<Vector2>();

        if (dpadDir.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
        }
        else if (dpadDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }

        //JunpFromObject();
    }

    private void OnActionJumpStarted(CallbackContext c)
    {
        if (jumpHandler.isJumping == false && isOnGround)
        {
            jumpHandler.StartJump();
            //StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
        }
    }

    private void OnActionJumpCancelled(CallbackContext c)
    {
        jumpHandler.CancelJumpImpulse();
    }

    private void OnActionAction(CallbackContext c)
    {

    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            sprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            sprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }
}