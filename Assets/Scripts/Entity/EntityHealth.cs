using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    protected Entity entity;
    protected EntityVFX entityVFX;

    [SerializeField] protected float currentHp = 100f;
    [SerializeField] protected bool isDead = false;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        entityVFX = GetComponent<EntityVFX>();
    }

    public virtual void TakeDamage(float damage, Transform attacker)
    {
        if (isDead)
            return;

        ReduceHp(damage); // 체력이 감소한다.
        entityVFX?.OnHit(); // 이벤트를 사용해 의존성을 제거하는 게 더 나을 것 같다.
    }

    protected void ReduceHp(float damage)
    {
        // 현재 체력이 0 이하가 되면 죽는다.
        currentHp -= damage;
        if (currentHp <= 0)
            Die(); 
    }

    protected void Die()
    {
        currentHp = 0f;
        isDead = true;
        entity.EnterDeadState(); // Dead 상태로 트랜지션.
    }
}
