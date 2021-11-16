using UnityEngine;
using UnityEngine.SceneManagement;

namespace JohannesMP
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneReference sceneToLoad;

        private void Start()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}