using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerUnmountingState : PlayerState
{
    [SerializeField] private float delay = .5f;
    private IEnumerator waitCoroutine;
    public override void Enter()
    {
        base.Enter();
        // start coroutine / wait 1 sec
        // change to move state
        waitCoroutine = Wait();
        player.StartCoroutine(waitCoroutine);
    }

    public override void Exit()
    {
        base.Exit();
        player.StopCoroutine(waitCoroutine);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        
        player.Unmount();
        player.Activate();
        fsm.ChangeState<PlayerMoveState>();
    }
}
