using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;

    [SerializeField] private Animator animator;
    public UnityEvent onEnabled;
    public UnityEvent onDisabled;
    public bool IsActivated { get; private set; }


    [Header("Debug")]
    [SerializeField] private int numberOfVisits = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numberOfVisits == 0)
        {
            IsActivated = true;
            animator.SetBool("pressed", true);
            onEnabled?.Invoke();
            //Wwise switchButton on
            playerAudio.switchButtonOn.Post(this.gameObject);
        }
        numberOfVisits++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        numberOfVisits--;
        if (numberOfVisits == 0)
        {
            IsActivated = false;
            animator.SetBool("pressed", false);
            onDisabled?.Invoke();
            //Wwise switchButton off
            playerAudio.switchButtonOff.Post(this.gameObject);
        }
    }
}
