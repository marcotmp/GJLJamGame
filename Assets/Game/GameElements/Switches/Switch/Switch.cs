using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    public UnityEvent OnSwitchActivated;
    private bool isActive = false;

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            OnSwitchActivated?.Invoke();
        }
    }
}
