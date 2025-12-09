using UnityEngine;

public class EnemyGroundState : EnemyState
{
    public EnemyGroundState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Update()
    {
        base.Update();

        // 플레이어를 감지하면 Battle 상태로 트랜지션.
        if (enemy.DetectPlayer())
        {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }

}
