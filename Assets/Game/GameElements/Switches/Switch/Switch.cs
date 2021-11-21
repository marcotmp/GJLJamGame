using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;

    public UnityEvent OnSwitchActivated;
    private bool isActive = false;

    public void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            OnSwitchActivated?.Invoke();
            playerAudio.switchButtonOn.Post(this.gameObject);
        }
    }
}
