using UnityEngine;

public abstract class EntityState
{
    // 모든 상태는 플레이어를 참조한다.
    protected Player player;

    // 모든 상태는 상태 머신을 참조한다.
    protected StateMachine stateMachine;

    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
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
