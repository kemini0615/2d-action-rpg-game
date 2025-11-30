using UnityEngine;

public class PlayerAirState : EntityState
{
    public PlayerAirState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        player.Move(player.MoveDirection.x * player.moveSpeed, player.Rigidbody.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
