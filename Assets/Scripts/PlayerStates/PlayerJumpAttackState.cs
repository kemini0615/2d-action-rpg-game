using UnityEngine;

public class PlayerJumpAttackState : EntityState
{
    public PlayerJumpAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}
    private bool hasLanded;

    public override void Enter()
    {
        base.Enter();

        hasLanded = false;
        player.Move(player.JumpAttackMovingVelocity.x * player.FacingDirection, player.JumpAttackMovingVelocity.y);
    }

    public override void Update()
    {
        base.Update();
        
        if (player.OnGround && !hasLanded)
        {
            hasLanded = true;
            player.Animator.SetTrigger("jumpAttackTrigger"); // 땅에 착지하면서 점프 공격 애니메이션 마무리.
            player.Move(0f, 0f);
        }

        if (AnimationEventTriggered && player.OnGround)
        {
            stateMachine.ChangeState(player.IdleState); // Idle 상태로 트랜지션.
        }
    }
}
