using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHelicopter : MonoBehaviour, IPlayer
{
    public InputActionAsset playerInput;
    public InputAction move;
    public PlayerSelector playerSelector;
    public BoxDetector groundDetector;
    public GameObject selectorIcon;
    public Animator anim;
    [HideInInspector] public Rigidbody2D rb;
    private FiniteStateMachine fsm;

    private void Awake() 
    {
        var actionMap = playerInput.FindActionMap("Gameplay");
        move = actionMap.FindAction("Move");
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
        Debug.Log($"{name} -> Activate");

        selectorIcon.SetActive(true);
        fsm.ChangeState(typeof(HeliActivate));
    }

    public void Deactivate()
    {
        Debug.Log($"{name} -> Deactivate");
        
        selectorIcon.SetActive(false);
        fsm.ChangeState(typeof(HeliDeactivate));
    }
}
