using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Transform> points;

#if UNITY_EDITOR
    void OnEnable()
    {
        GetPoints();
    }
#endif

    private void GetPoints()
    {
        points = GetComponentsInChildren<Transform>().ToList();
        points.RemoveAt(0);
    }

}
