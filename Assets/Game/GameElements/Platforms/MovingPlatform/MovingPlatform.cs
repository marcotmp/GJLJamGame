using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private PlayerAudioData playerAudio;
    [SerializeField] private Path path;
    [SerializeField] private Transform platform;
    [SerializeField] private float stopDelay = 1;
    private Transform currentPoint;
    private Transform nextPoint;

    private FiniteStateMachine fsm;

    public float duration = 1;
    private float t = 0;
    public int nextPointIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        PickPoints();

        fsm = new FiniteStateMachine();
        // fsm = new FiniteStateMachine<MovingPlatformBaseState>();
        // fsm.paramsInjector = (state)=>{state.movingPlatform = this};
        fsm.AddState(new IdleState() { movingPlatform = this });
        fsm.AddState(new MoveState() { movingPlatform = this });
        fsm.AddState(new WaitState(stopDelay, typeof(MoveState)));
        fsm.ChangeState<IdleState>();
    }

    public void Activate()
    {
        fsm.ChangeState<MoveState>();
        //playerAudio.elevatorOn.Post(platform.gameObject);
        //playerAudio.elevatorOn.Post(gameObject);
    }

    private void OnDestroy()
    {
        // commented code because platform.gameObject is destroyed before the MovingPlatform system
        //playerAudio.elevatorOff.Post(platform.gameObject);
        //playerAudio.elevatorOff.Post(gameObject);
    }

    public void PickPoints()
    {
        currentPoint = path.points[0];
        nextPoint = path.points[nextPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update();
    }

    public void Move()
    {
        if (!ReachDestination())
        {
            t += Time.deltaTime;
            // move to next node
            platform.position = Vector2.Lerp(currentPoint.position, nextPoint.position, t / duration);
        }
        else
            t = 0;
    }

    public void PickNextDestination()
    { 
        // pick next point
        nextPointIndex++;
        if (nextPointIndex > path.points.Count - 1)
        {
            nextPointIndex = 0;
        }
        currentPoint = nextPoint;
        nextPoint = path.points[nextPointIndex];
    }

    internal bool ReachDestination()
    {
        return t > duration;
    }
}

public class MovingPlatformBaseState : State
{
    public MovingPlatform movingPlatform;
}

public class IdleState : MovingPlatformBaseState
{
}

public class MoveState : MovingPlatformBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log(movingPlatform.nextPointIndex);
    }

    public override void Update()
    {
        base.Update();
        movingPlatform.Move();
        if (movingPlatform.ReachDestination())
            fsm.ChangeState<WaitState>();
    }

    public override void Exit()
    {
        base.Exit();
        movingPlatform.PickNextDestination();
    }
}