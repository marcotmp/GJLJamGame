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

        player.action.canceled += OnActionMountEnded;
    }

    public override void Exit()
    {
        base.Exit();
        player.StopCoroutine(waitCoroutine);

        player.action.canceled -= OnActionMountEnded;
    }

    void OnActionMountEnded(CallbackContext c)
    {
        fsm.ChangeState<PlayerMoveState>();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(delay);
        if (player.IsCollidingVehicle())
        {
            player.MountAction();
            fsm.ChangeState<PlayerMountedState>();
        }
        else 
            fsm.ChangeState<PlayerMoveState>();
    }
}
