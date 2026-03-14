using UnityEngine;

public class Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        
        IdleState = new EnemyIdleState(this, stateMachine, "idle");
        MoveState = new EnemyMoveState(this, stateMachine, "move");
        AttackState = new EnemyAttackState(this, stateMachine, "attack");
        BattleState = new EnemyBattleState(this, stateMachine, "battle");
        DeadState = new EnemyDeadState(this, stateMachine, "idle"); // 애니메이터 파라미터는 중요하지 않다.
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(IdleState); // 상태 머신 초기화.
    }

    public override void EnterDeadState()
    {
        base.EnterDeadState();

        stateMachine.ChangeState(DeadState);
    }
}
