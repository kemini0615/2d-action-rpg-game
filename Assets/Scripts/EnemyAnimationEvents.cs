using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private Enemy enemy;
    private Combat combat;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        combat = GetComponentInParent<Combat>();
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
