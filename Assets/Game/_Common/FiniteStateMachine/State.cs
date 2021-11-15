public interface IState
{
    void Exit();
    void Update();
    void Enter();
    void FixedUpdate();
    void SetFSM(FiniteStateMachine finiteStateMachine);
}

public class State : IState
{
    protected FiniteStateMachine fsm;

    virtual public void Exit()
    {
    }

    virtual public void Update()
    {
    }

    virtual public void FixedUpdate()
    {
    }

    virtual public void Enter()
    {
    }

    public void SetFSM(FiniteStateMachine finiteStateMachine)
    {
        fsm = finiteStateMachine;
    }
}
