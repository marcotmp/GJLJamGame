using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(fileName = "PlayerSelector", menuName = "ScriptableObjects/PlayerSelector")]
public class PlayerSelector : ScriptableObject
{
    [SerializeField] private InputAction changePlayerAction;

    private List<IPlayer> playerList;
    private int index = 0;

    public void OnEnable()
    {
        playerList = new List<IPlayer>();
        index = 0;

        // Activate 
        changePlayerAction.Enable();
        changePlayerAction.performed += OnChangePlayer;
    }

    private void OnDisable()
    {        
        changePlayerAction.performed -= OnChangePlayer;
        changePlayerAction.Disable();
    }

    public void Add(IPlayer player)
    {
        if (playerList.Count > 0)
            player.Deactivate();
        else
            player.Activate();

        playerList.Add(player);
    }

    private void OnChangePlayer(CallbackContext cc)
    {
        playerList[index].Deactivate();        
        index = ++index % playerList.Count;
        playerList[index].Activate();
    }
}
