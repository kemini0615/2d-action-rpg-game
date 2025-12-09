using UnityEngine;

public class EnemyMoveState : EnemyGroundState
{
    public EnemyMoveState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Update()
    {
        base.Update();

        // 적을 움직인다.
        enemy.Move(enemy.MoveSpeed * enemy.FacingDirection, Rigidbody.linearVelocity.y);

        // 벽 또는 길 끝에 도달하면 방향 전환하고 Idle 상태로 트랜지션.
        if (!enemy.OnGround || enemy.OnWall)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
