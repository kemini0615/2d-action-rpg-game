using UnityEngine;

public class PlayerAttackState : EntityState
{
    private int comboIndex = 0;
    private float lastAttackTime = 0f;

    public PlayerAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        // 콤보 공격 시간이 지나면 첫번째 공격으로 리셋.
        if (lastAttackTime + player.ComboDuration < Time.time)
        {
            comboIndex = 0;
        }

        player.Animator.SetInteger("comboIndex", comboIndex % 3);

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

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastAttackTime = Time.time;
    }
}
