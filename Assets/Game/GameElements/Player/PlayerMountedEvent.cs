using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMountedEvent", menuName = "ScriptableObjects/PlayerMountedEvent")]
public class PlayerMountedEvent : ScriptableObject
{
    public Action<GameObject> onPlayerMounted;
    public void Raise(GameObject vehicle)
    {
        onPlayerMounted?.Invoke(vehicle);
    }
}
