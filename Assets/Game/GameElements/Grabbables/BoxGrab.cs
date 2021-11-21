using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrab : MonoBehaviour, IGrabbable
{
    public Transform offset;
    public Rigidbody2D rigidbody;
    public float minSpeedToPlayHit = 0f;
    public PlayerAudioData playerAudio;
    private bool shouldProduceSound = false;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > minSpeedToPlayHit)
            PlayHitSound();
    }

    public void Release()
    {
        
    }
}
