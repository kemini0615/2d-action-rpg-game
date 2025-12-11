using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;
    private Combat combat;

    void Awake()
    {
        player = GetComponentInParent<Player>();
        combat = GetComponentInParent<Combat>();
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
