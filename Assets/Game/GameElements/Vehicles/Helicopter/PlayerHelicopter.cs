using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHelicopter : MonoBehaviour, IVehicle
{
    public InputActionAsset playerInput;
    [HideInInspector] public InputAction move;
    [HideInInspector] public InputAction grab;
    // public PlayerSelector playerSelector;
    public BoxDetector groundDetector;
    Vector3 groundDetectorInitialPos;
    public BoxDetector itemDetector;
    public GameObject selectorIcon;
    public Animator anim;
    //Wwise events
    /*[SerializeField] private AK.Wwise.Event grabObject = null;
    [SerializeField] private AK.Wwise.Event releaseObject = null;*/
    [SerializeField] private PlayerAudioData playerAudio;

    [SerializeField] private Transform handPosition;
    [SerializeField] public Joint2D joint;

    [HideInInspector] public Rigidbody2D rb;
    private FiniteStateMachine fsm;
    private IGrabbable objectGrabbed;
    private InputActionMap actionMap;
    public float grabOffset = 1.2f;

    private void Awake() 
    {
        actionMap = playerInput.FindActionMap("HelicopterControls");
        move = actionMap.FindAction("Move");
        grab = actionMap.FindAction("Grab");
        actionMap.Enable();
        rb = GetComponent<Rigidbody2D>();

        fsm = new FiniteStateMachine();
        fsm.AddState(new HeliActivate(this));
        fsm.AddState(new HeliMove(this));
        fsm.AddState(new HeliDeactivate(this));

        // playerSelector.Add(this);
    }

    private void OnDestroy()
    {
        fsm.GetCurrentState().Exit();
    }

    private void Start() {
        fsm.ChangeState(typeof(HeliDeactivate));
        groundDetectorInitialPos = groundDetector.transform.localPosition;
    }

    private void FixedUpdate() 
    {
        fsm.FixedUpdate();
    }

    public void MountPlayer(PlayerController player)
    {
        //actionMap.Enable();
        selectorIcon.SetActive(true);
        fsm.ChangeState(typeof(HeliActivate));
        PlayMountSound();
        //player.akGameObj.transform.parent = transform;
    }

    public void Deactivate()
    {
        //actionMap.Disable();
        Debug.Log("Deactivating Heli");
        selectorIcon.SetActive(false);
        fsm.ChangeState(typeof(HeliDeactivate));

    }

    public void UnmountPlayer()
    {
        Deactivate();
        Release();
        PlayUnmountSound();
    }

    public void PlayMountSound()
    {
        playerAudio.heliGetIn.Post(this.gameObject);
    }

    public void PlayUnmountSound()
    {
        playerAudio.heliGetOut.Post(this.gameObject);
    }

    private bool isHeliMoveStartPlaying = false;
    public void PlayMoveStart()
    {
        if (!isHeliMoveStartPlaying)
        {
            playerAudio.heliMoveStart.Post(this.gameObject);
            isHeliMoveStartPlaying = true;
        }
    }

    public void MoveStop()
    {
        if (isHeliMoveStartPlaying)
        {
            playerAudio.heliMoveStop.Post(this.gameObject);
            isHeliMoveStartPlaying = false;
        }
    }


    private IGrabbable GetObjectToGrab()
    {
        var playersFound = itemDetector.raycastHit2DAll;
        foreach (var rh in playersFound)
        {
            Debug.Log($"Finding Objects to grab {rh.collider.name}");
            if (rh.collider.gameObject != this.gameObject)
            {                
                return rh.collider.gameObject.GetComponent<IGrabbable>();
            }
        }

        return null;
    }

    public void Grab()
    {
        Debug.Log("Grab");

        if (itemDetector.CheckCollisionAll())
        {
            objectGrabbed = GetObjectToGrab();
            //objectGrabbed?.Grab();
            if (objectGrabbed != null)
            {
                //objectGrabbed.transform.position = handPosition.position;
                // attach joint to other
                joint.enabled = true;
                joint.connectedBody = objectGrabbed.gameObject.GetComponent<Rigidbody2D>();
                groundDetector.transform.position = objectGrabbed.GetGrabOffset();
                playerAudio.heliGrabObj.Post(this.gameObject);
            }

            
        }
    }

    public bool ObjectInHand()
    {
        return objectGrabbed != null;
    }

    public void Release()
    {
        //objectGrabbed?.Release();
        if (objectGrabbed != null)
        {
            objectGrabbed = null;
            joint.enabled = false;
            joint.connectedBody = null;
            groundDetector.transform.localPosition = groundDetectorInitialPos;
            playerAudio.heliReleaseObj.Post(this.gameObject);
        }
    }

    bool DetectObjectInGround()
    {
        var itemFound = groundDetector.raycastHit2DAll;
        foreach (var rh in itemFound)
        {
            if (rh.collider.gameObject != this.gameObject)
            {                
                return true;
            }
        }
        return false;
    }
}
