using UnityEngine;

public class PlayerDashState : EntityState
{
    public PlayerDashState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        timer = player.DashDuration;
    }

    public override void Update()
    {
        base.Update();

        player.Move(player.FacingDirection * player.dashSpeed, 0f);

        // Idle 상태로 트랜지션.
        if (timer < 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
