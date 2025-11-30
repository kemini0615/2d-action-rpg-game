using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void OnAttackFinished()
    {
        player.AttackState.SetAnimationEventTriggered(true);
    }
}
