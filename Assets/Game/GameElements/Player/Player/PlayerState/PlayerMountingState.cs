using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[System.Serializable]
public class PlayerMountingState : PlayerState
{
    [SerializeField] private float delay = .5f;
    private IEnumerator waitCoroutine;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.ProcessStop();
    }

    public override void Enter()
    {
        base.Enter();
        waitCoroutine = Wait();
        player.StartCoroutine(waitCoroutine);
    }

    public override void Exit()
    {
        base.Exit();
        player.StopCoroutine(waitCoroutine);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        player.MountAction();
        fsm.ChangeState<PlayerMountedState>();
    }
}
