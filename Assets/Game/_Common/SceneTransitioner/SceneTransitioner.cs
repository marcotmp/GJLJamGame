using DUCK.Tween.Extensions;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EscapeFromAntrio
{
    public class SceneTransitioner : MonoBehaviour
    {
        [SerializeField] private SceneTransitionerChannel transitionerChannel;
        [SerializeField] private Image veil;
        [SerializeField] private float fadeInDuration = 2;
        [SerializeField] private float fadeOutDuration = 2;

        //[SerializeField] private Animator animator;
        public bool isPlaying { get; private set; } = false;
        public Action OnSceneFadeOut;

        private string scene;

        private void Awake()
        {
            transitionerChannel.OnLoadScene += LoadScene;
        }

        private void Start()
        {
            var a = veil.Fade(1f, 0f, fadeInDuration);
            Time.timeScale = 0;

            a.Play(() =>
            {
                Time.timeScale = 1;
            });
        }

        private void OnDestroy()
        {
            transitionerChannel.OnLoadScene -= LoadScene;
        }

        //public event Action
        public void LoadScene(string scene)
        {
            this.scene = scene;

            //animator.Play("FadeIn");
            Debug.Log("Fade out to " + scene);
            var a = veil.FadeTo(1f, fadeOutDuration);
            a.Play(()=>
            {
                Debug.Log("Loading Scene " + scene);

                SceneManager.LoadScene(scene);
                Time.timeScale = 1;
            });

            isPlaying = true;
        }

        private void A_OnFadeIn()
        {
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
            //animator.Play("FadeOut");
            isPlaying = false;

        }

        private void A_OnFadeOut()
        {
            isPlaying = false;

            // Maybe let the player move again?
            OnSceneFadeOut?.Invoke();
        }
    }
}
