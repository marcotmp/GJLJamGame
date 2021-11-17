using UnityEngine;

// attach to platform
public class AttachToPlatform : MonoBehaviour
{
    private GameObject passenger;
    private Rigidbody2D rb;

    private Vector3 oldPosition;
    // uncomment this code to get the conveyor behaviour
    //public Vector3 conveyorDelta;

    private void Start()
    {
        oldPosition = transform.position;
    }

    void LateUpdate()
    {
        if (passenger)
        {
            var delta = transform.position - oldPosition;
            if (rb)
                rb.position += (Vector2)delta;
            else
                passenger.transform.position += delta;

            //passenger.transform.position += + conveyorDelta;
        }

        oldPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsOnTop(collision))
        {
            passenger = collision.gameObject;
            rb = passenger.GetComponent<Rigidbody2D>();
        }
    }

    private bool IsOnTop(Collision2D collision)
    {
        // test contact points to check if player is colliding from the top
        for (int i = 0; i < collision.contactCount; i++)
        {
            var c = collision.GetContact(i);
            return (c.normal.y < 0); // if normal is less than 0, it's on top.
        }
        return false;
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        passenger = null;
    }

}

