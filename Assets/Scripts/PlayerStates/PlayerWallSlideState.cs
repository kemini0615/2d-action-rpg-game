using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Update()
    {
        base.Update();

        if (player.MoveDirection.y < 0)
            player.Move(player.MoveDirection.x, player.Rigidbody.linearVelocity.y); // 벽타기 상태에서 아래키를 누르면 빠르게 내려가기.
        else
            player.Move(player.MoveDirection.x, player.Rigidbody.linearVelocity.y * player.WallSlideFallMultiplier); // 벽타기 상태에서 아래키를 누르지 않으면 느리게 내려가기.

        if (player.OnGround)
        {
            stateMachine.ChangeState(player.IdleState); // Idle 상태로 트랜지션.
            player.Flip();
        }

        if (!player.OnWall)
            stateMachine.ChangeState(player.FallState); // Fall 상태로 트랜지션.

        if (player.InputActions.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.WallJumpState); // WallJump 상태로 트랜지션.
    }
}
