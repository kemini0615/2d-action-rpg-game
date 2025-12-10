using UnityEngine;

public class EnemyBattleState : EnemyState
{
    private Transform target;

    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        if (target == null)
            target = enemy.DetectPlayer().transform;
    }

    public override void Update()
    {
        base.Update();

        Animator.SetFloat("xVelocity", Rigidbody.linearVelocity.x);

        if (TargetInAttackRange())
            stateMachine.ChangeState(enemy.AttackState);
        else
            enemy.Move(enemy.BattleMoveSpeed * GetBattleMoveDirection(), Rigidbody.linearVelocity.y);
    }

    private float GetDistanceToPlayer()
    {
        if (target == null)
            return float.MaxValue;

        return Mathf.Abs(target.position.x - enemy.transform.position.x);
    }

    private bool TargetInAttackRange() => GetDistanceToPlayer() < enemy.AttackRange;

    private int GetBattleMoveDirection()
    {
        if (target == null)
            return 0;

        return target.position.x > enemy.transform.position.x ? 1 : -1;
    }
}
