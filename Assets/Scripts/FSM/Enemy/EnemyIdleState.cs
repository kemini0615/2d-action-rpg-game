using UnityEngine;

public class EnemyIdleState : EnemyGroundState
{
    public EnemyIdleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        timer = enemy.IdleDuration; // 정지 시간 타이머 설정.

        // Idle 상태가 시작될 때 적을 정지.
        enemy.Move(0f, 0f);
    }

    public override void Update()
    {
        base.Update();

        if (timer < 0)
            stateMachine.ChangeState(enemy.MoveState); // Move 상태로 트랜지션.
    }
}
