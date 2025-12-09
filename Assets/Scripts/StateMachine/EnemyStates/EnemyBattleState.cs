using UnityEngine;

public class EnemyBattleState : EnemyState
{
    public EnemyBattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Enter: Battle State");
    }


    public override void Update()
    {
        base.Update();
    }




}
