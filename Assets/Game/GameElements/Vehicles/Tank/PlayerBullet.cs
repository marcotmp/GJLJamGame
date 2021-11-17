using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private Rigidbody2D rb;

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
