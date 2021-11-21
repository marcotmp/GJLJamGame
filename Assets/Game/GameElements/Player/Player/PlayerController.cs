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
    [SerializeField] public AkGameObj akGameObj;
    [SerializeField] public PlayerAudioData playerAudio;

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
    // [HideInInspector] public InputAction jump;
    [HideInInspector] public InputAction action;

    private Vector2 dpadDir;
    private bool isOnGround;

    private bool facingRight = false;

    // private bool isActive = false;

    private FiniteStateMachine fsm;

    IVehicle vehicle;

    private void Awake()
    {
        actionMap = inputActionAsset.FindActionMap("Gameplay");
        move = actionMap.FindAction(moveId);
        // jump = actionMap.FindAction("Jump");
        action = actionMap.FindAction("ChangeCharacter");
        // action.performed += OnMountAction;
        move.performed += Move;

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
        // Remove any listener on the current state
        // This is used to fix a bug when changing levels the move event is connected after the object is destroyed
        fsm.GetCurrentState().Exit();
        move.performed -= Move;
    }

    private void FixedUpdate()
    {
        fsm.FixedUpdate();

        //ToggleSign();
    }

    //private void ToggleSign()
    //{ 
    //    // toggle sign
    //    var vehicleDetector = this.vehicleDetector.CheckCollision();

    //    if (vehicleDetector)
    //        selectorIcon.SetActive(true);
    //    else if (!vehicleDetector && selectorIcon.activeSelf)
    //        selectorIcon.SetActive(false);
    //}

    public float yDelta = 0;

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

        motion.y += yDelta;

        bool wasOnGround = isOnGround;
        isOnGround = groundDetector.CheckCollision();
        if (!wasOnGround && isOnGround) motion.y = 0;

        rigidbody.velocity = motion;
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

        rigidbody.velocity = motion;
    }

    public bool IsOnGround => isOnGround;

    public void Move(CallbackContext ctx)
    {
        dpadDir = ctx.ReadValue<Vector2>();
        
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

        if (dpadDir.x != 0 && !isWheelsMovePlaying)
        {
            playerAudio.wheelsMoveStart.Post(gameObject);
            isWheelsMovePlaying = true;
        }
        else if (Mathf.Approximately(dpadDir.x, 0) && isWheelsMovePlaying)
        {
            playerAudio.wheelsMoveStop.Post(gameObject);
            isWheelsMovePlaying = false;
        }
    }

    private bool isWheelsMovePlaying = false;

    // public void StartJump()
    // {
    //     motion.y = jumpImpulse;
    // }

    // public void CancelJump()
    // {
    //     if (motion.y > 0) motion.y = 0;
    // }

    // private bool isMounted = false;

    public void MountAction()
    {   
        if (vehicleDetector.CheckCollision())
        {
            var obj = vehicleDetector.raycastHit2D;
            vehicle = obj.collider.GetComponent<IVehicle>();

            // mount player on the vehicle;
            vehicle.MountPlayer(this);
            // gameObject.SetActive(false);
            // isMounted = true;
            playerMountedEvent.Raise(obj.collider.gameObject);
            Deactivate();
        }
    }

    public bool IsCollidingVehicle()
    {
        return vehicleDetector.CheckCollision();
    }

    public void Unmount()
    {
        // isMounted = false;
        // gameObject.SetActive(true);
        // fsm.ChangeState<PlayerUnmountingState>();
        Debug.Log("Player unmounting!");
        playerMountedEvent.Raise(gameObject);
        vehicle.UnmountPlayer();
        transform.position = vehicle.transform.position;
        vehicle = null;
    }

    public void Activate()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        sprite.enabled = true;
        //GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<BoxCollider2D>().enabled = true;
        vehicleDetector.GetComponent<BoxDetector>().enabled = true;
    }

    void Deactivate()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        sprite.enabled = false;
        //GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().enabled = false;
        vehicleDetector.GetComponent<BoxDetector>().enabled = false;
    }

    // IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    // {
    //     Vector3 originalSize = Vector3.one;
    //     Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
    //     float t = 0f;
    //     while (t <= 1.0)
    //     {
    //         t += Time.deltaTime / seconds;
    //         sprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
    //         yield return null;
    //     }
    //     t = 0f;
    //     while (t <= 1.0)
    //     {
    //         t += Time.deltaTime / seconds;
    //         sprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
    //         yield return null;
    //     }
    // }
}