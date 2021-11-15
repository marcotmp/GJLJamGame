using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.red;
    public Vector2 size = new Vector2(1, 1);
    public Vector2 offset = new Vector2(0, 0);

    [Header("")]
    public bool debug = false;
    public bool debugAll = false;
    public RaycastHit2D raycastHit2D;
    public RaycastHit2D[] raycastHit2DAll;
    private bool collided;
    private Vector3 hitPoint;
    private Vector3 normal;

    public Vector3 HitPoint => hitPoint;
    public Vector3 Normal => normal;

    public Vector3 StartPos()
    {
        //Vector3 facingXDir = new Vector3(System.Math.Sign(transform.localScale.x), 1);
        return transform.position;// + Vector3.Scale(offset, facingXDir);
    }

    public bool CheckCollision()
    {
        raycastHit2D = Physics2D.BoxCast(StartPos(), size, transform.rotation.eulerAngles.z, transform.right, 0, layerMask);
        collided = raycastHit2D.collider != null;

        hitPoint = raycastHit2D.point;
        normal = raycastHit2D.normal;

        return collided;
    }

    public bool CheckCollisionAll()
    {
        raycastHit2DAll = Physics2D.BoxCastAll(StartPos(), size, transform.rotation.eulerAngles.z, transform.right, 5, layerMask);

        collided = raycastHit2DAll.Length > 0;

        return collided;
    }

    private void OnDrawGizmos()
    {
        if (debug)
        {
            CheckCollision();
            if (debugAll)
                CheckCollisionAll();
        }

        var color = collided ? activeColor : inactiveColor;

        var right = transform.right * size.x;
        var up = transform.up * size.y;
        var halfRight = right / 2;
        var halfUp = up / 2;

        Vector3 topLeft = StartPos() - halfRight + halfUp;
        Vector3 bottomRight = StartPos() + halfRight - halfUp;

        Gizmos.color = color;
        Gizmos.DrawRay(topLeft, right);
        Gizmos.DrawRay(bottomRight, -right);
        Gizmos.DrawRay(topLeft, -up);
        Gizmos.DrawRay(bottomRight, up);

        Gizmos.color = Color.blue;
        if (debugAll)
        {
            for (int i = 0; i < raycastHit2DAll.Length; i++)
            {
                var contact = raycastHit2DAll[i];
                Gizmos.DrawWireCube(contact.point, new Vector3(1, 1));
            }
        }
    }
}
