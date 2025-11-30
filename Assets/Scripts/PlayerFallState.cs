public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // Idle 상태로 트랜지션.
        if (player.OnGround)
        {
            stateMachine.ChangeState(player.IdleState);
        }

        // Slide 상태로 트랜지션.
        if (player.OnWall)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
