using JohannesMP;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private SceneTransitionerChannel sceneTransition;
    [SerializeField] private SceneReference menuScene;
    [SerializeField] private PausePopup pausePopup;
    [SerializeField] private ConfirmationPopup confirmationPopup;

    private InputActionMap actionMap;
    private InputAction reset;
    private InputAction pause;

    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Gameplay");
        pause = actionMap.FindAction("Pause");
        reset = actionMap.FindAction("Reset");

        reset.Enable();
        pause.Enable();

        reset.performed += Input_OnReset;
        pause.performed += Input_OnPause;
    }

    void OnDestroy()
    {
        reset.performed -= Input_OnReset;
        pause.performed -= Input_OnPause;
    }

    private void Input_OnReset(CallbackContext ctx)
    {
        Reset();
    }

    private void Input_OnPause(CallbackContext ctx)
    {
        //confirmationPopup.Show();
        //confirmationPopup.onAccept = OnBackToMenu;
        //confirmationPopup.onCancel = () => pause.Enable();
        pause.Disable();
        //if (!pausePopup.IsOpen)
        {
            Time.timeScale = 0;
            // reset game

            pausePopup.Show();
            pausePopup.onContinue = OnContinue;
            pausePopup.onRestart = OnRestart;
            pausePopup.onBackToMenu = OnBackToMenu;
        }
    }

    private void OnContinue()
    {
        Time.timeScale = 1;
        pause.Enable();
    }

    private void OnRestart()
    {
        Reset();
    }

    private void OnBackToMenu()
    {
        pause.Enable();
        sceneTransition.LoadScene(menuScene);
    }

    private void Reset()
    {
        // reset game
        sceneTransition.LoadScene(SceneManager.GetActiveScene().path);
    }
}
