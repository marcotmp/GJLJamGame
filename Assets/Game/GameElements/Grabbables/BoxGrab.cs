using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrab : MonoBehaviour, IGrabbable
{
    public Transform offset;

    public float GetGrabOffset()
    {
        return offset.position.y * 2;
    }
}
