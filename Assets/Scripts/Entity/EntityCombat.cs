using UnityEngine;

public class EntityCombat : MonoBehaviour
{
    [SerializeField] private Transform targetChecker;
    [SerializeField] private float targetCheckerRadius = 1f;
    [SerializeField] private LayerMask targetLayer;

    // TEMP
    [SerializeField] private float damage = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetChecker.position, targetCheckerRadius);
    }

    private Collider2D[] DetectAllTargets()
    {
        return Physics2D.OverlapCircleAll(targetChecker.position, targetCheckerRadius, targetLayer);
    }

    public void PerformAttack()
    {
        Collider2D[] targetColliders = DetectAllTargets();

        foreach (Collider2D targetCollider in targetColliders)
        {
            EntityHealth targetHealth = targetCollider.GetComponent<EntityHealth>();
            targetHealth?.TakeDamage(damage);
        }
    }
}
