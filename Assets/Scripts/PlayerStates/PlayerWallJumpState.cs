using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        player.Move(player.WallJumpForce * -player.FacingDirection, player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (player.Rigidbody.linearVelocity.y < 0)
            stateMachine.ChangeState(player.FallState); // Fall 상태로 트랜지션.
    }

}
