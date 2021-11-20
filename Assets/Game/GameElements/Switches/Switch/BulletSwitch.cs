using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletSwitch : MonoBehaviour
{
    // Wwise bulletSwitch event
    [SerializeField]
    private AK.Wwise.Event bulletSwitch = null;
    // End Wwise code
    public UnityEvent OnSwitchActivated;
    public Animator anim;

    public void SwitchActivated()
    {
        OnSwitchActivated?.Invoke();
        anim.SetTrigger("Activate");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBullet>())
        {
            SwitchActivated();
            bulletSwitch.Post(gameObject);
        }
    }
}
