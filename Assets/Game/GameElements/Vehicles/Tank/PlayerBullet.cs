using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Rigidbody2D rb;

    [Header("Audio")]
    [SerializeField] private PlayerAudioData playerAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    public void SetRotation(Quaternion rotation)
    {        
        transform.rotation = rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroySelf();
        playerAudio.tankShotExplosion.Post(this.gameObject);

    }

    private void OnBecameInvisible()
    {
        DestroySelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
