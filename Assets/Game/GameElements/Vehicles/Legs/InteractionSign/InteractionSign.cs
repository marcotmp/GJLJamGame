using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSign : MonoBehaviour
{
    [SerializeField] private BoxDetector playerDetector;
    [SerializeField] private GameObject sign;

    private void Update()
    {
        var detectPlayer = playerDetector.CheckCollision();

        if (detectPlayer && !sign.activeSelf)
        {
            sign.SetActive(true);
        }
        else if (!detectPlayer && sign.activeSelf)
        {
            sign.SetActive(false);
        }
    }

    internal void Disable()
    {
        gameObject.SetActive(false);
    }

    internal void Enable()
    {
        gameObject.SetActive(true);
    }
}
