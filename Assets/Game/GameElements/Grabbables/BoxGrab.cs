using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrab : MonoBehaviour, IGrabbable
{
    public Transform offset;

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
