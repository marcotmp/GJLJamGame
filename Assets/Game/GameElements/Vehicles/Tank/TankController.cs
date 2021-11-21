using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class TankController : MonoBehaviour, IVehicle, IGrabbable
{
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private BoxDetector groundDetector;
    // [SerializeField] private PlayerSelector playerSelector;
    [SerializeField] private GameObject selectorIcon;
    [SerializeField] private PlayerCannon cannon;

    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;

    [Header("Speed")]
    [SerializeField] private float horizontalSpeed;
    private InputActionMap actionMap;
    private InputAction move;
    private InputAction unmount;
    private InputAction shoot;

    private Vector2 dpadDir;
    //[SerializeField] private bool isJumping = false;
    private bool isOnGround;

    private bool facingRight = false;
    private PlayerController player;

    private void Awake()
    {
        actionMap = inputActionAsset.FindActionMap("Gameplay");
        move = actionMap.FindAction("Move");
        unmount = actionMap.FindAction("ChangeCharacter");
        shoot = actionMap.FindAction("Shoot");
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        DisableControls();
    }

    private void FixedUpdate()
    {
        // move horizontally
        rigidbody.velocity = new Vector2(horizontalSpeed * dpadDir.x, rigidbody.velocity.y);

        //// if moving down
        //if (rigidbody.velocity.y <= 0)
        //{
        //    var wasOnGround = isOnGround;
        //    isOnGround = groundDetector.CheckCollision();

        //    if (isOnGround)
        //    {
        //        //if (!wasOnGround)
        //        //    StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        //    }
        //}
    }

    public void Deactivate()
    {
        selectorIcon.SetActive(false);
        DisableControls();
    }

    public void MountPlayer(PlayerController player)
    {
        Debug.Log("Tank Mount Player");

        this.player = player;
        selectorIcon.SetActive(true);
        EnableControls();
        playerAudio.tankGetIn.Post(this.gameObject);
    }

    private void EnableControls()
    {
        move.performed += OnActionMove;
        move.canceled += OnActionMove;
        // unmount.started += OnExitPlayer;
        shoot.performed += OnShootAction;
    }

    private void DisableControls()
    {
        move.performed -= OnActionMove;
        move.canceled -= OnActionMove;
        // unmount.started -= OnExitPlayer;
        shoot.performed -= OnShootAction;
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
    }

    // private void OnExitPlayer(CallbackContext c)
    // {
    //     // player.transform.position = transform.position;
    //     selectorIcon.SetActive(false);
    //     // actionMap.Disable();
    //     // player.Unmount();
    //     Deactivate();
    // }

    public void UnmountPlayer()
    {
        Debug.Log("Tank Unmount Player");
        selectorIcon.SetActive(false);
        Deactivate();
        playerAudio.tankGetOut.Post(this.gameObject);
    }

    private void OnShootAction(CallbackContext c)
    {
        cannon.Shoot();
        playerAudio.tankShot.Post(this.gameObject);
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

    public Vector3 GetGrabOffset()
    {
        return groundDetector.transform.position;
    }

    public void Grab()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }
}