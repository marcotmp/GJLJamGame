using JohannesMP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "ScriptableObjects/LevelProgression")]
public class LevelProgression : ScriptableObject
{
    public string CurrentLevel;// { get; private set; }
    public SceneReference finalScene;

    internal void OpenLevel(string path)
    {
        // only store path
        if (path != finalScene.ScenePath)
        {
            // store level to open
            CurrentLevel = path;
            PlayerPrefs.SetString("CurrentLevel", path);
        }
    }

    internal void Clear()
    {
        CurrentLevel = "";
        PlayerPrefs.SetString("CurrentLevel", "");
        PlayerPrefs.DeleteKey("CurrentLevel");
    }
}
