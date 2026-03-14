using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();

        // 애니메이션 비활성화를 통해 애니메이션 재생을 정지한다.
        Animator.enabled = false;

        // 아래쪽으로 떨어지게 만든다.
        enemy.GetComponent<Collider2D>().enabled = false;
        enemy.Rigidbody.gravityScale = 12f;
        enemy.Rigidbody.linearVelocity = new Vector2(0f, 15f);
    }
}
