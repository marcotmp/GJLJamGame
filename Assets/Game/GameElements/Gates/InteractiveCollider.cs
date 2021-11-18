using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveCollider : MonoBehaviour
{
    public UnityEvent OnTriggerEntered2D;
    [HideInInspector] public GameObject other;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        this.other = other.gameObject;
        OnTriggerEntered2D?.Invoke();
    }
}
