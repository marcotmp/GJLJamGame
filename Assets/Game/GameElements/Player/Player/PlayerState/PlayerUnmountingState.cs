using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUnmountingState : PlayerState
{
    [SerializeField] private float delay = .5f;
    public override void Enter()
    {
        base.Enter();
        // start coroutine / wait 1 sec
        // change to move state
        player.StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        player.actionMap.Enable();
        fsm.ChangeState<PlayerMoveState>();        
    }
}
