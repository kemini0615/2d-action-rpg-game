using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.Move(player.MoveDirection.x * player.MoveSpeed, player.Rigidbody.linearVelocity.y);

        if (player.InputActions.Player.Attack.WasPressedThisFrame())
            stateMachine.ChangeState(player.JumpAttackState); // JumpAttack 상태로 트랜지션.
    }

    public override void Exit()
    {
        base.Exit();
    }
}
