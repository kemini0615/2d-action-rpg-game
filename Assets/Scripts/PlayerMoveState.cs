// Move 상태는 Ground 상태의 자식
public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    // 상태가 시작될 때 호출된다.
    public override void Enter()
    {
        base.Enter();
    }

    // 상태가 유지될 때 호출된다.
    public override void Update()
    {
        base.Update();

        if (player.MoveDirection.x == 0)
            stateMachine.ChangeState(player.IdleState); // Idle 상태로 트랜지션.

        // 플레이어를 움직인다.
        player.Move(player.MoveDirection.x * player.MoveSpeed, player.Rigidbody.linearVelocity.y);
    }

    // 상태가 종료될 때 호출된다.
    public override void Exit()
    {
        base.Exit();
    }
}
