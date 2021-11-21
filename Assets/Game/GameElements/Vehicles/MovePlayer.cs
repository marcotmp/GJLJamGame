using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
        Deactivate();
    }

    void Update()
    {
        player.position = transform.position;
    }

    public void Activate(Transform transform)
    {
        gameObject.SetActive(true);
        player = transform;
    }

    internal void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
