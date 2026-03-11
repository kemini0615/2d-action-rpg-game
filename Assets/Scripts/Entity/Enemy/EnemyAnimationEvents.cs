using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private Enemy enemy;
    private EntityCombat combat;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        combat = GetComponentInParent<EntityCombat>();
    }

    public void OnAttack()
    {
        combat.PerformAttack();
    }

    public void OnAttackFinished()
    {
        enemy.AttackState.SetAnimationEventTriggered(true);
    }
}
