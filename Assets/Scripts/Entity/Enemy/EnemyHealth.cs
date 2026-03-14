using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage, Transform player)
    {
        base.TakeDamage(damage, player);

        // 죽었다면 다른 행동을 막는다.
        // 예: 다른 상태로의 트랜지션
        if (isDead)
            return;

        enemy.EnterBattleState(player);
    }
}
