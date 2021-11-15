using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveCollider : MonoBehaviour
{
    public UnityEvent OnTriggerEntered2D;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        OnTriggerEntered2D?.Invoke();
    }
}
