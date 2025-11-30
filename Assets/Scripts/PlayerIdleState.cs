// Idle 상태는 Ground 상태의 자식
public class PlayerIdleState : PlayerGroundState
{
    // 부모 클래스의 생성자에 매개변수가 존재하는 경우, 'base()'가 필수적이다.
    // 부모 클래스의 생성자가 매개변수가 없는 기본 생성자의 경우, 'base()'는 생략 가능하다.
    public PlayerIdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    // 상태가 시작될 때 호출된다.
    public override void Enter()
    {
        base.Enter();

        // Idle 상태가 시작될 때 플레이어를 정지.
        player.Move(0f, 0f);
    }

    // 상태가 유지될 때 호출된다.
    public override void Update()
    {
        base.Update();

        // Move 상태로 트랜지션.
        if (player.MoveDirection.x != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    // 상태가 종료될 때 호출된다.
    public override void Exit()
    {
        base.Exit();
    }
}