using UnityEngine;

public class PlayerDashState : EntityState
{
    private float originalGravityScale;

    public PlayerDashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        // 대시 시간 설정.
        timer = player.DashDuration;

        // Dash 상태가 시작될 때 중력 무시.
        originalGravityScale = player.Rigidbody.gravityScale;
        player.Rigidbody.gravityScale = 0f;
    }

    public override void Update()
    {
        base.Update();

        player.Move(player.FacingDirection * player.DashSpeed, 0f);

        if (timer < 0 || player.OnWall)
        {
            if (player.OnGround)
                stateMachine.ChangeState(player.IdleState); // Idle 상태로 트랜지션.
            else
                stateMachine.ChangeState(player.FallState); // Fall 상태로 트랜지션.
        }
    }

    public override void Exit()
    {
        base.Exit();

        // Dash 상태가 종료될 때 플레이어를 정지.
        player.Move(0f, 0f);

        // Dash 상태가 종료될 때 중력 복원.
        player.Rigidbody.gravityScale = originalGravityScale;
    }
}
 