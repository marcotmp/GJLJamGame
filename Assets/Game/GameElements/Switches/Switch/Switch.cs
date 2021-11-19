using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent OnSwitchActivated;
    private void Start()
    {
        Debug.Log("Switch.Start");
    }

    public void Activate()
    {
        Debug.Log("Switch.Activate");
        OnSwitchActivated?.Invoke();
    }
}
