public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        player.Move(player.Rigidbody.linearVelocity.x, player.jumpForce);
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

    public override void Exit()
    {
        base.Exit();
    }
}
