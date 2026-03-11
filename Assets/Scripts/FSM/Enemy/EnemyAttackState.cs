using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        enemy.Move(0f, 0f);

        if (AnimationEventTriggered)
            stateMachine.ChangeState(enemy.BattleState); // Battle 상태로 트랜지션.
    }
}
