public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        player.Move(player.Rigidbody.linearVelocity.x, player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        // 한 프레임 안에서 2개의 트랜지션이 발생하는 버그 수정.
            // 1. Air(Jump) 상태 → JumpAttack 상태
            // 2. Jump 상태 → Fall 상태
        if (player.Rigidbody.linearVelocity.y < 0 && stateMachine.CurrentState != player.JumpAttackState)
            stateMachine.ChangeState(player.FallState); // Fall 상태로 트랜지션.
    }

    public override void Exit()
    {
        base.Exit();
    }
}
