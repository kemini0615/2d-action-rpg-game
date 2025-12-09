using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void OnAttackFinished()
    {
        enemy.AttackState.SetAnimationEventTriggered(true);
    }
}
