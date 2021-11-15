using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public void GoalReached()
    {
        Debug.Log("Level Win!");
    }

    public void GoalActivated()
    {
        gameObject.SetActive(true);
    }
}
