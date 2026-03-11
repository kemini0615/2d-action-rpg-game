using UnityEngine;

public class EnemyBattleState : EnemyState
{
    private Transform target;
    private float targetLastDetected;

    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        if (target == null)
            target = enemy.DetectPlayer().transform;

        if (ShouldRetreat())
            enemy.Retreat(GetChaseDirection());
    }

    public override void Update()
    {
        base.Update();

        if (enemy.DetectPlayer())
            targetLastDetected = Time.time;

        if (TargetInAttackRange())
            stateMachine.ChangeState(enemy.AttackState); // 플레이어가 사거리 안에 있다면 Attack 상태로 트랜지션.
        else if (BattleIsOver())
            stateMachine.ChangeState(enemy.IdleState); // Battle 상태가 끝나면 Idle 상태로 트랜지션.
        else
            enemy.Move(enemy.BattleMoveSpeed * GetChaseDirection(), Rigidbody.linearVelocity.y); // 그렇지 않다면 플레이어 추적.
    }



    // 플레이어와의 거리 계산.
    private float GetDistanceToPlayer()
    {
        if (target == null)
            return float.MaxValue;

        return Mathf.Abs(target.position.x - enemy.transform.position.x);
    }

    // 플레이어가 사거리 안에 있는지 확인.
    private bool TargetInAttackRange() => enemy.DetectPlayer() && (GetDistanceToPlayer() < enemy.AttackRange);

    // Battle 상태 끝났는지 확인.
    private bool BattleIsOver() => Time.time > targetLastDetected + enemy.BattleDuration;

    // 뒤로 물러나야하는지 확인.
    private bool ShouldRetreat() => GetDistanceToPlayer() < enemy.RetreatDistance;


    // 배틀 상태에서 플레이어 추적 방향 계산.
    private int GetChaseDirection()
    {
        if (target == null)
            return 0;

        return target.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
