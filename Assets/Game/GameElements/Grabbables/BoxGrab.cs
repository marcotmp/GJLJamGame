using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrab : MonoBehaviour, IGrabbable
{
    public Transform offset;
    public Rigidbody2D rigidbody;
    public float maxFallSpeed = 0f;
    public PlayerAudioData playerAudio;
    private bool shouldProduceSound = false;

    private void Update()
    {
        if (shouldProduceSound)
        {
            if (Mathf.Approximately(rigidbody.velocity.y, 0))
            {
                PlayHitSound();
            }
        }

        if (rigidbody.velocity.y < -maxFallSpeed)
        {
            shouldProduceSound = true;
        }
        else
            shouldProduceSound = false;

    }

    public void PlayHitSound()
    {
        Debug.Log("PlayHitSound");
        playerAudio.boxGroundHit.Post(gameObject);
    }

    public Vector3 GetGrabOffset()
    {
        return offset.position;
    }

    public void Grab()
    {
        
    }

    public void Release()
    {
        
    }
}
