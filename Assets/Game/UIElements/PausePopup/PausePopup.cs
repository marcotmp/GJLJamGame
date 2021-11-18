using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private ForceUIFocus forceFocus;
    [SerializeField] private InputAction cancelAction;

    public Action onContinue;
    public Action onRestart;
    public Action onBackToMenu;

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
        Debug.Log("PausePopup . Show");
        gameObject.SetActive(true);
        forceFocus.SelectDefaultButton();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Button_OnContinue()
    {
        Hide();
        onContinue?.Invoke();
    }

    public void Button_OnRestart()
    {
        Hide();
        onRestart?.Invoke();
    }

    public void Button_OnBackToMenu()
    {
        Hide();
        onBackToMenu?.Invoke();
    }

    private void OnCancelButtonTriggered(CallbackContext c)
    {
        Hide();
        onContinue();
    }

    public bool IsOpen => gameObject.activeSelf;
    
}
