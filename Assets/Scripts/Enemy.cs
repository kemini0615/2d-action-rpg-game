using UnityEngine;

public class Enemy : Entity
{
    public EnemyIdleState IdleState { get; protected set; } // 정지 상태.
    public EnemyMoveState MoveState { get; protected set; } // 이동 상태.

    [field: SerializeField] public float IdleDuration { get; private set; } = 2f; // 정지 상태 지속 시간.
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f; // 적 이동 속력.
}
