using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletSwitch : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;

    public UnityEvent OnSwitchActivated;
    public Animator anim;

    public void SwitchActivated()
    {
        OnSwitchActivated?.Invoke();
        anim.SetTrigger("Activate");
        playerAudio.switchButtonOn.Post(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBullet>())
        {
            SwitchActivated();
        }
    }
}
