using JohannesMP;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelProgression", menuName = "ScriptableObjects/LevelProgression")]
public class LevelProgression : ScriptableObject
{
    public string CurrentLevel { get; private set; }

    internal void OpenLevel(string path)
    {
        // store level to open
        CurrentLevel = path;
    }

    internal void Clear()
    {
        CurrentLevel = "";
    }
}
