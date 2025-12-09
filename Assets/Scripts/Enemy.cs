using UnityEngine;

public class Enemy : Entity
{
    public EnemyIdleState IdleState { get; protected set; } // 정지 상태.
    public EnemyMoveState MoveState { get; protected set; } // 이동 상태.
    public EnemyAttackState AttackState { get; protected set; } // 공격 상태.
    public EnemyBattleState BattleState { get; protected set; } // 전투 상태.

    [field: SerializeField] public float IdleDuration { get; private set; } = 2f; // 정지 상태 지속 시간.
    [field: SerializeField] public float MoveSpeed { get; private set; } = 1.5f; // 이동 속력.
    [Range(0, 2)][field: SerializeField] public float MoveAnimationSpeedMultiplier { get; private set; } = 1f; // Move 애니메이션 재생 속도 계수.

    [SerializeField] private Transform playerChecker;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float distanceToPlayer = 10f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerChecker.position, playerChecker.position + Vector3.right * distanceToPlayer * FacingDirection);
    }

    public RaycastHit2D DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(playerChecker.position, Vector2.right * FacingDirection, distanceToPlayer, playerLayer | groundLayer);

        // 레이캐스트 대상이 없는 경우.
        if (hit.collider == null) // if (hit == false)와 동일.
            return default; // RaycastHit2D의 기본값 반환.

        // 레이캐스트 대상이 플레이어가 아닌 경우.
        if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            return default;

        return hit;
    }
}
