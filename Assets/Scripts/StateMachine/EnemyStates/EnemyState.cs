public class EnemyState : EntityState
{
    protected Enemy enemy;

    public EnemyState(Enemy enemy, StateMachine stateMachine, string stateName) : base(stateMachine, stateName)
    {
        this.enemy = enemy;
        this.Animator = enemy.Animator;
        this.Rigidbody = enemy.Rigidbody;
    }

    public override void Update()
    {
        base.Update();

        Animator.SetFloat("moveAnimationSpeedMultiplier", enemy.MoveAnimationSpeedMultiplier);
    }
}
