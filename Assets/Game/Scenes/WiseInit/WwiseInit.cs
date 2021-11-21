using JohannesMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WwiseInit : MonoBehaviour
{
    [SerializeField] private SceneReference nextScene;

    private void Start()
    {
        SceneManager.LoadScene(nextScene);
    }
}
