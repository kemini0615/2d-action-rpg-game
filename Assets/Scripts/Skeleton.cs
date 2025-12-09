using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        
        IdleState = new EnemyIdleState(this, stateMachine, "idle");
        MoveState = new EnemyMoveState(this, stateMachine, "move");
        AttackState = new EnemyAttackState(this, stateMachine, "attack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState); // 상태 머신 초기화.
    }
}
