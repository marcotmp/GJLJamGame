using JohannesMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] public SceneReference nextScene;
    [SerializeField] public SceneTransitionerChannel transition;
    [SerializeField] public WinEventChannel winEventChannel;
    [SerializeField] public float delay = 1;

    public void GoalReached()
    {
        Debug.Log("Level Win!");

        // save that you completed current level
        // levelStorage.WinLevel();

        // stop the player and trigger win animation
        winEventChannel.Raise();

        StartCoroutine(WinSequence());
    }

    public void GoalActivated()
    {
        gameObject.SetActive(true);
    }

    private IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(delay);

        transition.LoadScene(nextScene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure only the player can activate the goal
        var player = collision.gameObject.GetComponent<PlayerController>();
        if (player)
            GoalReached();
    }
}
