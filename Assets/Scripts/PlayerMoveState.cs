using UnityEngine;

public class PlayerMoveState : EntityState
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

        // 상태 트랜지션(Move -> Idle) 발생.
        if (Input.GetKeyDown(KeyCode.F))
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    // 상태가 종료될 때 호출된다.
    public override void Exit()
    {
        base.Exit();
    }
}
