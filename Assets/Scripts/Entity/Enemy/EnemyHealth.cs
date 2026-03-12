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
        enemy.EnterBattleState(player);
    }
}
