using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public UnityEvent onEnabled;
    public UnityEvent onDisabled;


    [Header("Debug")]
    [SerializeField] private int numberOfVisits = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numberOfVisits == 0)
        {
            // when this object is pressed, anim press
            animator.SetBool("pressed", true);
            onEnabled?.Invoke();
        }
        numberOfVisits++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        numberOfVisits--;
        if (numberOfVisits == 0)
        {
            animator.SetBool("pressed", false);
            onDisabled?.Invoke();
        }
    }
}
