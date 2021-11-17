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
    public PlayerSelector playerSelector;
    public BoxDetector groundDetector;
    public BoxDetector playerDetector;
    public GameObject selectorIcon;
    public Animator anim;

    [SerializeField] private Transform handPosition;
    [SerializeField] public Joint2D joint;

    [HideInInspector] public Rigidbody2D rb;
    private FiniteStateMachine fsm;
    private GameObject objectGrabbed;
    private InputActionMap actionMap;

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

        playerSelector.Add(this);
    }

    private void Start() {
        fsm.ChangeState(typeof(HeliDeactivate));
    }

    private void FixedUpdate() 
    {
        fsm.FixedUpdate();
    }

    public void Activate()
    {
        actionMap.Enable();
        selectorIcon.SetActive(true);
        fsm.ChangeState(typeof(HeliActivate));
        rb.mass = 1f;
    }

    public void Deactivate()
    {
        actionMap.Disable();
        selectorIcon.SetActive(false);
        fsm.ChangeState(typeof(HeliDeactivate));
        rb.mass = 0.1f;
    }


    private GameObject GetObjectToGrab()
    {
        var playersFound = playerDetector.raycastHit2DAll;
        foreach (var rh in playersFound)
        {
            Debug.Log($"Finding Objects to grab {rh.collider.name}");
            if (rh.collider.gameObject != this.gameObject)
            {
                return rh.collider.gameObject;//.GetComponent<IDraggable>();
            }
        }

        return null;
    }

    public void Grab()
    {
        Debug.Log("Grab");

        if (playerDetector.CheckCollisionAll())
        {
            objectGrabbed = GetObjectToGrab();
            //objectGrabbed?.Grab();
            if (objectGrabbed)
            {
                //objectGrabbed.transform.position = handPosition.position;
                // attach joint to other
                joint.enabled = true;
                joint.connectedBody = objectGrabbed.GetComponent<Rigidbody2D>();
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
        if (objectGrabbed)
        {
            objectGrabbed = null;
            joint.enabled = false;
        }
    }
}
