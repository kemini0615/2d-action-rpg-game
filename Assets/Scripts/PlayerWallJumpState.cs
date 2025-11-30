using UnityEngine;

public class PlayerWallJumpState : EntityState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        player.Move(player.wallJumpForce * -player.FacingDirection, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        // Fall 상태로 트랜지션.
        if (player.Rigidbody.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

}
