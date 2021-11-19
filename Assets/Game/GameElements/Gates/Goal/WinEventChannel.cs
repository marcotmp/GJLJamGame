using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WinEventChannel", menuName = "ScriptableObjects/WinEventChannel")]
public class WinEventChannel : ScriptableObject
{
    public event Action onWin;

    public void Raise()
    {
        onWin?.Invoke();
    }
}
