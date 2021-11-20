using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    Vector3 GetGrabOffset();
    void Grab();
    void Release();
    GameObject gameObject {get;}
}
