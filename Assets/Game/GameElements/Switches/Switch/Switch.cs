using MarcoTMP;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;
    [SerializeField] private Animator animator;

    public UnityEvent OnSwitchActivated;
    
    private bool isActive = false;

    public void Activate()
    {
        if (!isActive)
        {
            animator.SetTrigger("Activate");
            isActive = true;
            OnSwitchActivated?.Invoke();
            playerAudio.switchButtonOn.Post(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DebugConsole.Log("Switch.OnTriggerEnter2D");
        Activate();
    }
}
