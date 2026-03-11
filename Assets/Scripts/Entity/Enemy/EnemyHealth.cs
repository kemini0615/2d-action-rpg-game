using UnityEngine;

public class EnemyHealth : EntityHealth
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(float damage, Transform player)
    {
        base.TakeDamage(damage, player);
        enemy.EnterBattleState(player);
    }
}
