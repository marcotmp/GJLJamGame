using UnityEngine;
using UnityEngine.Events;

public class AndGate : MonoBehaviour
{
    public UnityEvent<bool> onValueChange;
    public UnityEvent onEnabled;
    public UnityEvent onDisabled;

    public bool output { get; private set; }
    public bool inputA = false;
    public bool inputB = false;

    public void SetInputA(bool value)
    {
        inputA = value;
        Evaluate();
    }

    public void SetInputB(bool value)
    {
        inputB = value;
        Evaluate();
    }

    private void Evaluate()
    {
        output = inputA && inputB;

        Debug.Log($"Evaluate {output}");
        onValueChange.Invoke(output);
        if (output)
            onEnabled?.Invoke();
        else
            onDisabled?.Invoke();
    }
}
