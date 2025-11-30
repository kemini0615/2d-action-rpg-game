using UnityEngine;

public abstract class EntityState
{
    // 모든 상태는 플레이어를 참조한다.
    protected Player player;

    // 모든 상태는 상태 머신을 참조한다.
    protected StateMachine stateMachine;

    // 애니메이션의 트랜지션을 위한 파라미터로 사용된다.
    protected string stateName;

    // 모든 상태는 타이머를 갖는다.
    protected float timer;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    // 상태가 시작될 때 호출된다.
    public virtual void Enter()
    {
        player.Animator.SetBool(this.stateName, true); // 애니메이션 재생 시작.
    }

    // 상태가 유지될 때 호출된다.
    public virtual void Update()
    {
        timer -= Time.deltaTime; // 타이머 시간 감소.

        player.Animator.SetFloat("yVelocity", player.Rigidbody.linearVelocity.y);

        // Dash 상태로 트랜지션.
        if (player.InputActions.Player.Dash.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    // 상태가 종료될 때 호출된다.
    public virtual void Exit()
    {
        player.Animator.SetBool(this.stateName, false); // 애니메이션 재생 종료.
    }
}
