using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    private EntityVFX entityVFX;

    [SerializeField] protected float currentHp = 100f;
    [SerializeField] protected bool isDead = false;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<EntityVFX>();
    }

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        if (isDead)
            return;

        ReduceHp(damage);
        entityVFX?.OnHit(); // 이벤트를 사용해 의존성을 제거하는 게 더 나을 것 같다.
    }

    protected void ReduceHp(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    protected void Die()
    {
        currentHp = 0f;
        isDead = true;
        Debug.Log(gameObject.name + " Died!");
    }
}
