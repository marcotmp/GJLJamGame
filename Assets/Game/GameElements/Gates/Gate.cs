using System;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event gateEvent = null;
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    // TODO: rename to just Open
    // Use past tense only on events
    [Obsolete]
    public void GateOpened()
    {
        Open();
    }

    // Do open animation
    public void Open()
    {
        Debug.Log("Gate.Open");
        anim?.SetBool("Open", true);
        gateEvent.Post(gameObject);

    }

    // Do close animation
    public void Close()
    {
        anim?.SetBool("Open", false);
    }
}
