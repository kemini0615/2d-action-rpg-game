using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    [SerializeField] protected float currentHp = 100f;
    [SerializeField] protected bool isDead = false;

    public virtual void TakeDamage(float damage)
    {
        if (isDead)
            return;

        ReduceHp(damage);
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
