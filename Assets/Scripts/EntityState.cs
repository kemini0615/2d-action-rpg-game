using UnityEngine;

public class EntityState
{
    // 모든 상태는 상태 머신을 참조한다.
    protected StateMachine StateMachine { get; private set; }

    private string stateName;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        StateMachine = stateMachine;
        this.stateName = stateName;
    }

    // 상태가 시작될 때 호출된다.
    public virtual void Enter()
    {
        Debug.Log(this.stateName + " Enter.");
    }

    // 상태가 유지될 때 호출된다.
    public virtual void Update()
    {
        Debug.Log(this.stateName + " Update.");
    }

    // 상태가 종료될 때 호출된다.
    public virtual void Exit()
    {
        Debug.Log(this.stateName + " Exit.");
    }
}
