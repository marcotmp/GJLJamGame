using JohannesMP;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private SceneTransitionerChannel sceneTransition;
    //[SerializeField] private SceneReference menuScene;

    private InputActionMap actionMap;
    private InputAction reset;

    void Start()
    {
        actionMap = inputActionAsset.FindActionMap("Gameplay");
        reset = actionMap.FindAction("Reset");
        reset = actionMap.FindAction("Reset");

        reset.Enable();

        reset.performed += OnReset;
    }

    void OnDestroy()
    {
        reset.performed -= OnReset;
    }

    private void OnReset(CallbackContext ctx)
    {
        // reset game
        sceneTransition.LoadScene(SceneManager.GetActiveScene().path);
    }

}
