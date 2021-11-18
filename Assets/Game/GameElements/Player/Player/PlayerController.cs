using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private BoxDetector groundDetector;
    [SerializeField] private BoxDetector vehicleDetector;
    [SerializeField] private GameObject selectorIcon;

    [Header("Speed")]
    public float horizontalSpeed;

    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float jumpImpulse = 10f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float decceleration = 15f;
    [SerializeField] private float gravityUp = 30f;
    [SerializeField] private float gravityDown = 20f;
    [SerializeField] private float gravityScale = 8f;

    private Vector2 motion = Vector2.zero;

    [SerializeField] private string moveId = "Move";

    [Header("States")]
    [SerializeField] private PlayerMoveState moveState;
    [SerializeField] private PlayerJumpState jumpState;
    [SerializeField] private PlayerMountedState mountedState;
    [SerializeField] private PlayerMountingState mountingState;
    [SerializeField] private PlayerUnmountingState unmountedState;


    [Header("events")]
    [SerializeField]
    private PlayerMountedEvent playerMountedEvent;
    [HideInInspector] public InputActionMap actionMap;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction jump;
    [HideInInspector] public InputAction action;

    private Vector2 dpadDir;
    private bool isOnGround;

    private bool facingRight = false;

    private bool isActive = false;

    private FiniteStateMachine fsm;

    private void Awake()
    {
        actionMap = inputActionAsset.FindActionMap("Gameplay");
        move = actionMap.FindAction(moveId);
        jump = actionMap.FindAction("Jump");
        action = actionMap.FindAction("ChangeCharacter");
        action.performed += OnMountAction;

        actionMap.Enable();

        // States
        fsm = new FiniteStateMachine();
        moveState.player = this;
        jumpState.player = this;
        mountingState.player = this;
        mountedState.player = this;
        unmountedState.player = this;
        fsm.AddState(moveState);
        fsm.AddState(jumpState);
        fsm.AddState(mountingState);
        fsm.AddState(mountedState);
        fsm.AddState(unmountedState);

        fsm.ChangeState(moveState);
    }

    private void OnDestroy()
    {
        action.started -= OnMountAction;
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate();

        ToggleSign();
    }

    private void ToggleSign()
    { 
        // toggle sign
        var vehicleDetector = this.vehicleDetector.CheckCollision();
        
        if (vehicleDetector)
            selectorIcon.SetActive(true);
        else if (!vehicleDetector && selectorIcon.activeSelf)
            selectorIcon.SetActive(false);
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

        rigidbody.velocity = motion;
    }

    public bool IsOnGround => isOnGround;

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

    private bool isMounted = false;
    private void OnMountAction(CallbackContext c)
    {
        Debug.Log($"{c.phase}");
        
        if (vehicleDetector.CheckCollision())
        {
            var obj = vehicleDetector.raycastHit2D;
            var vehicle = obj.collider.GetComponent<IVehicle>();

            // mount player on the vehicle;
            vehicle.MountPlayer(this);
            gameObject.SetActive(false);
            isMounted = true;
            fsm.ChangeState<PlayerMountedState>();
            playerMountedEvent.Raise(obj.collider.gameObject);
        }
    }

    public void Unmount()
    {
        isMounted = false;
        gameObject.SetActive(true);
        fsm.ChangeState<PlayerUnmountingState>();
        playerMountedEvent.Raise(gameObject);
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