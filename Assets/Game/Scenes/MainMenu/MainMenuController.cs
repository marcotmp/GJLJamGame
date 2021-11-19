using JohannesMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneTransitionerChannel transition;
    [SerializeField] private SceneReference firstLevel;
    [SerializeField] private ConfirmationPopup confirmationPopup;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button playButton;
    [SerializeField] private LevelProgression levelProgression;
    [SerializeField] private ForceUIFocus focus;

    private void Start()
    {
        // if no progress... show the play button
        var levelToLoad = levelProgression.CurrentLevel;
        if (string.IsNullOrEmpty(levelToLoad))
        {
            // show play
            continueButton.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            playButton.Select();
            focus.SetDefaultButton(playButton);
        }
        else
        {
            // show continue
            continueButton.gameObject.SetActive(true);
            continueButton.Select();
            focus.SetDefaultButton(continueButton);
            newGameButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
        }
    }

    public void Button_OnContinue()
    {
        // continue game on last level
        transition.LoadScene(levelProgression.CurrentLevel);
    }

    public void Button_OnNewGame()
    {
        // show yes / no popup
        confirmationPopup.Show();
        confirmationPopup.onAccept = OnResetGameAccepted;
    }

    public void Button_OnPlay()
    {
        transition.LoadScene(firstLevel);
    }

    private void OnResetGameAccepted()
    {
        levelProgression.Clear();
        transition.LoadScene(firstLevel);
    }
}
