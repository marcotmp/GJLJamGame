using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class ConfirmationPopup : MonoBehaviour
{
    [SerializeField] private ForceUIFocus forceFocus;
    [SerializeField] private InputAction cancelAction;

    public Action onAccept;
    public Action onCancel;
    
    private void Start()
    {
        cancelAction.Enable();
        cancelAction.performed += OnCancelButtonTriggered;
    }

    private void OnDestroy()
    {
        cancelAction.performed -= OnCancelButtonTriggered;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        forceFocus.SelectDefaultButton();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        Hide();
        onCancel?.Invoke();
    }

    public void Accept()
    {
        Hide();
        onAccept?.Invoke();
    }

    private void OnCancelButtonTriggered(CallbackContext c)
    {
        Cancel();
    }
}
