using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[CreateAssetMenu(fileName = "PlayerSelector", menuName = "ScriptableObjects/PlayerSelector")]
public class PlayerSelector : ScriptableObject
{
    [SerializeField] private InputAction changePlayerAction;

    private List<PlayerController> playerList;
    private int index = 0;

    public void OnEnable()
    {
        Debug.Log("OnEnable");

        playerList.Clear();
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

    public void Add(PlayerController player)
    {
        Debug.Log($"{player.name} -> {playerList.Count}");
        if (playerList.Count > 0)
            player.Deactivate();
        else
            player.Activate();

        playerList.Add(player);
    }

    private void OnChangePlayer(CallbackContext cc)
    {
        Debug.Log("OnChangePlayer");
        playerList[index].Deactivate();
        //index++;
        //if (index > playerList.Count)
        //    index = 0;

        index = (++index) % playerList.Count;
        playerList[index].Activate();
    }
}
