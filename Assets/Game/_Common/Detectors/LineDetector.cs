using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDetector : MonoBehaviour
{
    public LayerMask layerMask;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.red;
    public float distance = 1;

    public bool debug = false;
    [SerializeField] private bool useLossyScale = false;
    [SerializeField] private bool handleAsVertical = false;

    [Header("Debug")]
    [SerializeField] private Vector3 direction;
    [SerializeField] private bool colliding;
    [SerializeField] private float length;
    [SerializeField] public float hLength;
    [SerializeField] public float vLength;
    [HideInInspector] public RaycastHit2D raycastHit2D;
    [HideInInspector] public RaycastHit2D[] raycastHit2DAll;
    [HideInInspector] public Vector2 point;


    private bool collided;


    public bool CheckCollision()
    {
        if (useLossyScale)
        {
            if (!handleAsVertical)
                direction = Vector3.Scale(transform.right, transform.lossyScale);
            else
                direction = new Vector3(
                    transform.right.x * transform.lossyScale.y,
                    transform.right.y * transform.lossyScale.x,
                    transform.right.z * transform.lossyScale.z
                    );
        }
        else
            direction = transform.right;

        raycastHit2D = Physics2D.Raycast(transform.position, direction, distance, layerMask);
        collided = raycastHit2D.collider;
        point = raycastHit2D.point;

        return collided;
    }

    public bool CheckCollisionAll()
    {
        if (useLossyScale)
        {
            if (!handleAsVertical)
                direction = Vector3.Scale(transform.right, transform.lossyScale);
            else
                direction = new Vector3(
                    transform.right.x * transform.lossyScale.y,
                    transform.right.y * transform.lossyScale.x,
                    transform.right.z * transform.lossyScale.z
                    );
        }
        else
            direction = transform.right;

        raycastHit2DAll = Physics2D.RaycastAll(transform.position, direction, distance, layerMask);
        collided = raycastHit2DAll.Length > 0;
        if (collided)
            point = raycastHit2DAll[0].point;


        return collided;
    }

    /// <summary>
    /// Returns the point or if not colliding, the fartest point in the line.
    /// </summary>
    /// <returns></returns>
    public Vector3 EndPoint()
    {
        if (collided)
            return point;
        else
            return transform.position + direction * distance;
    }

    public float HorizontalLength()
    {
        length = hLength = Mathf.Abs(EndPoint().x - transform.position.x);
        return length;
    }

    public float VerticalLength()
    {
        length = vLength = Mathf.Abs(EndPoint().y - transform.position.y);
        return length;
    }


    private void OnDrawGizmos()
    {
        if (debug)
        {
            colliding = CheckCollision();
            //colliding = CheckCollisionAll();
            HorizontalLength();
            VerticalLength();
        }

        var color = collided ? activeColor : inactiveColor;
   
        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, direction * distance);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(EndPoint(), 0.2f);

        //for (int i = 0; i < raycastHit2DAll.Length; i++)
        //{
        //    var contact = raycastHit2DAll[i];
        //    Gizmos.DrawWireSphere(contact.point, 0.05f);            
        //}

    }
}
