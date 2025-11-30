using UnityEngine;

public class PlayerAttackState : EntityState
{
    public PlayerAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        timer = player.MovingAttackDuration;
    }

    public override void Update()
    {
        base.Update();

        if (timer >= 0)
        {
            // 움직이면서 공격 가능.
            player.Move(player.movingAttackSpeed * player.FacingDirection, player.Rigidbody.linearVelocity.y);
        }
        else
        {
            // 정지.
            player.Move(0f, player.Rigidbody.linearVelocity.y);
        }

        // Idle 상태로 트랜지션.
        if (AnimationEventTriggered)
        {
            stateMachine.ChangeState(player.IdleState);
            SetAnimationEventTriggered(false);
        }
    }
}
