using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public UnityEvent<bool> onSet;

    [Header("Debug")]
    [SerializeField] private int numberOfVisits = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numberOfVisits == 0)
        {
            // when this object is pressed, anim press
            animator.SetBool("pressed", true);
            onSet?.Invoke(true);
        }
        numberOfVisits++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        numberOfVisits--;
        if (numberOfVisits == 0)
        {
            animator.SetBool("pressed", false);
            onSet?.Invoke(false);
        }
    }
}
