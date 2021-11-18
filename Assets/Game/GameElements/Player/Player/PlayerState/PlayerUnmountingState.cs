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

        player.action.canceled += OnActionUnmountEnded;
    }

    public override void Exit()
    {
        base.Exit();
        player.StopCoroutine(waitCoroutine);

        player.action.canceled -= OnActionUnmountEnded;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        fsm.ChangeState<PlayerMoveState>();
        player.Unmount();
    }

    void OnActionUnmountEnded(CallbackContext c)
    {
        fsm.ChangeState<PlayerMountedState>();
    }
}
