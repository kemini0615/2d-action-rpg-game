using UnityEngine;

public class PlayerGroundState : EntityState
{
    public PlayerGroundState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // Jump 상태로 트랜지션.
        if (player.InputActions.Player.Jump.WasPressedThisFrame())
        {
            stateMachine.ChangeState(player.JumpState);
        }

        // Fall 상태로 트랜지션
        if (player.Rigidbody.linearVelocity.y < 0 && !player.OnGround)
        {
            stateMachine.ChangeState(player.FallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
