using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    private EntityCombat combat;

    void Awake()
    {
        player = GetComponentInParent<Player>();
        combat = GetComponentInParent<EntityCombat>();
    }

    public void OnAttack()
    {
        combat.PerformAttack();
    }

    public void OnAttackFinished()
    {
        player.AttackState.SetAnimationEventTriggered(true);
    }

    public void OnJumpAttackFinished()
    {
        player.JumpAttackState.SetAnimationEventTriggered(true);
    }
}
