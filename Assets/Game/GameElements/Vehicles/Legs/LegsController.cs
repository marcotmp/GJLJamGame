using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegsController : MonoBehaviour, IVehicle
{
    public BoxDetector groundDetector;
    public GameObject selectorIcon;
    //public InteractionSign interactionSign;

    public InputActionAsset playerInput;
    [HideInInspector] public InputActionMap actionMap;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction jump;

    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator anim;

    private FiniteStateMachine fsm;

    [Header("States")]
    [SerializeField] private LegsActivate activateState;
    [SerializeField] private LegsMove moveState;
    [SerializeField] private LegsAir airState;
    [SerializeField] private LegsDeactivate deactivateState;

    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float jumpImpulse = 10f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float decceleration = 15f;
    [SerializeField] private float gravityUp = 30f;
    [SerializeField] private float gravityDown = 20f;
    [SerializeField] private float gravityScale = 8f;

    private Vector2 motion = Vector2.zero;
    private Vector2 dpadDir;
    private bool isOnGround;
    private bool facingRight = false;
    public bool IsOnGround => isOnGround;

    private void Awake() 
    {
        actionMap = playerInput.FindActionMap("Gameplay");
        move = actionMap.FindAction("Move");
        jump = actionMap.FindAction("Jump");

        fsm = new FiniteStateMachine();

        activateState.legs = this;
        moveState.legs = this;
        airState.legs = this;
        deactivateState.legs = this;

        fsm.AddState(activateState);
        fsm.AddState(moveState);
        fsm.AddState(airState);
        fsm.AddState(deactivateState);

        fsm.ChangeState(deactivateState);
    }

    private void FixedUpdate() 
    {
        fsm.FixedUpdate();    
    }

    public void ProcessMove()
    {
        if (dpadDir.x != 0)
        {
            motion.x += dpadDir.x * acceleration * Time.deltaTime;
        }
        else
        {
            if (isOnGround) motion.x = Mathf.MoveTowards(motion.x, 0, decceleration * Time.deltaTime);
        }
        
        motion.x = Mathf.Clamp(motion.x, -maxSpeed, maxSpeed);

        if (!isOnGround)
        {
            if (motion.y > 0) motion.y -= gravityUp * Time.deltaTime;
            else motion.y -= gravityDown * Time.deltaTime;
            motion.y = Mathf.Max(motion.y, -gravityScale);
        }

        bool wasOnGround = isOnGround;
        isOnGround = groundDetector.CheckCollision();
        if (!wasOnGround && isOnGround) motion.y = 0;

        rb.velocity = motion;
    }

    public void ProcessStop()
    {
        if (isOnGround) motion.x = Mathf.MoveTowards(motion.x, 0, decceleration * Time.deltaTime);
        
        motion.x = Mathf.Clamp(motion.x, -maxSpeed, maxSpeed);

        if (!isOnGround)
        {
            if (motion.y > 0) motion.y -= gravityUp * Time.deltaTime;
            else motion.y -= gravityDown * Time.deltaTime;
            motion.y = Mathf.Max(motion.y, -gravityScale);
        }

        bool wasOnGround = isOnGround;
        isOnGround = groundDetector.CheckCollision();
        if (!wasOnGround && isOnGround) motion.y = 0;

        rb.velocity = motion;
    }

    public void Move(Vector2 dir)
    {
        dpadDir = dir;
        
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
    }

    public void StartJump()
    {
        motion.y = jumpImpulse;
    }

    public void CancelJump()
    {
        if (motion.y > 0) motion.y = 0;
    }

    public void Deactivate()
    {
        selectorIcon.SetActive(false);
        fsm.ChangeState<LegsDeactivate>();
        dpadDir = Vector2.zero;
    }

    public void MountPlayer(PlayerController player)
    {
        selectorIcon.SetActive(true);
        fsm.ChangeState<LegsActivate>();
    }

    public void UnmountPlayer()
    {
        Deactivate();
    }
}
