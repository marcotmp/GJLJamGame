using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent OnSwitchActivated;
    public void SwitchActivated()
    {
        OnSwitchActivated?.Invoke();
    }
}
