using UnityEngine;

public class Enemy : Entity
{
    public EnemyIdleState IdleState { get; protected set; } // 정지 상태.
    public EnemyMoveState MoveState { get; protected set; } // 이동 상태.
    public EnemyAttackState AttackState { get; protected set; } // 공격 상태.


    [field: SerializeField] public float IdleDuration { get; private set; } = 2f; // 정지 상태 지속 시간.
    [field: SerializeField] public float MoveSpeed { get; private set; } = 1.5f; // 이동 속력.
    [Range(0, 2)][field: SerializeField] public float MoveAnimationSpeedMultiplier { get; private set; } = 1f; // Move 애니메이션 재생 속도 계수
}
