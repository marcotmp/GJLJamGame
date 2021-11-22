using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    public List<Transform> Points => points;
    void OnEnable()
    {
        GetPoints();
    }

    private void GetPoints()
    {
        points = GetComponentsInChildren<Transform>().ToList();
        points.RemoveAt(0);
    }

}
