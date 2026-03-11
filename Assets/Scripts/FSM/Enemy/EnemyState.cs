using UnityEngine;

public class EnemyState : BaseState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName) : base(stateMachine, stateName)
    {
        this.enemy = enemy;
        this.Animator = enemy.Animator;
        this.Rigidbody = enemy.Rigidbody;
    }

    public override void UpdateAnimatorParameter()
    {
        base.UpdateAnimatorParameter();

        Animator.SetFloat("xVelocity", Rigidbody.linearVelocity.x);
        Animator.SetFloat("moveAnimationSpeedMultiplier", enemy.MoveAnimationSpeedMultiplier);
        Animator.SetFloat("battleAnimationSpeedMultiplier", enemy.BattleMoveSpeed / enemy.MoveSpeed);
    }
}
