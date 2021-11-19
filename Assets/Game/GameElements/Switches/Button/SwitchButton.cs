using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public UnityEvent<bool> onSet;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when this object is pressed, anim press
        animator.SetBool("pressed", true);
        onSet?.Invoke(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("pressed", false);
        onSet?.Invoke(false);
    }
}
