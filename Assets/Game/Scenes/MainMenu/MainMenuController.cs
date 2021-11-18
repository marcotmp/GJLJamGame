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

    private void Start()
    {
        // if no progress... show the play button
        //continueButton.gameObject.SetActive(false);
        //newGameButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }

    public void Button_OnContinue()
    {
        // continue game on last level
        // find last level opened
        // var lastLevel = gameStorage.GetLastLevelOpened();
        // listOfLevels.GetLevel(lastLevel);
        transition.LoadScene(firstLevel);
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
        transition.LoadScene(firstLevel);
    }

}
