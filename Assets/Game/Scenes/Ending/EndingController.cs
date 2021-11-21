using JohannesMP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public SceneReference mainMenu;
    public SceneTransitionerChannel channel;
    public void Button_OnMainMenu()
    {
        channel.LoadScene(mainMenu.ScenePath);
    }
}
