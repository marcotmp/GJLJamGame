using System;
using UnityEngine;


public class Gate : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;
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
        playerAudio.gateOpen.Post(this.gameObject);

    }

    // Do close animation
    public void Close()
    {
        anim?.SetBool("Open", false);
    }
}
