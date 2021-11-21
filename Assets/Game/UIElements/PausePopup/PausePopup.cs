using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PausePopup : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;
    [SerializeField] private AK.Wwise.State stateMenu;
    [SerializeField] private AK.Wwise.State stateGame;

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
        //Wwise state to muffle sound
        stateMenu.SetValue();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        //Wwise state to unmuffle sound
        stateGame.SetValue();
    }

    public void Button_OnContinue()
    {
        Hide();
        onContinue?.Invoke();
    }

    public void Button_OnRestart()
    {
        //Hide();
        onRestart?.Invoke();
        playerAudio.restartAudio.Post(this.gameObject);
    }

    public void Button_OnBackToMenu()
    {
        //Hide();
        onBackToMenu?.Invoke();
    }

    private void OnCancelButtonTriggered(CallbackContext c)
    {
        Hide();
        onContinue();
    }

    public bool IsOpen => gameObject.activeSelf;
    
}
