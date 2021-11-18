using JohannesMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private SceneTransitionerChannel transition;
    [SerializeField] private SceneReference firstLevel;
    [SerializeField] private ConfirmationPopup confirmationPopup;


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

    private void OnResetGameAccepted()
    {
        transition.LoadScene(firstLevel);
    }

}
