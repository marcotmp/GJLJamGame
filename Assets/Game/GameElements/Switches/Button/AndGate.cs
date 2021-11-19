using UnityEngine;
using UnityEngine.Events;

public class AndGate : MonoBehaviour
{
    public UnityEvent onEnabled;
    public UnityEvent onDisabled;

    public SwitchButton[] switchButtons;
    
    private bool lastState = false;
    private bool isEnabled = false;

    private void Update()
    {
        lastState = isEnabled;
        isEnabled = Evaluate();
        if (isEnabled != lastState)
        {
            if (isEnabled)
                onEnabled?.Invoke();
            else
                onDisabled?.Invoke();
        }
    }

    private bool Evaluate()
    {
        foreach (var sb in switchButtons)
        {
            if (!sb.IsActivated) return false;
        }

        return true;
    }
}
