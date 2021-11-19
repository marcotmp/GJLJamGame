using System;
using UnityEngine;


    [CreateAssetMenu(fileName = "SceneTransitionerChannel", menuName = "ScriptableObjects/SceneTransitionerChannel")]
    public class SceneTransitionerChannel : ScriptableObject
    {
        public event Action<string> OnLoadScene;

        public void LoadScene(string scene)
        {
            OnLoadScene?.Invoke(scene);
        }
    }
