using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어는 자신의 상태 머신을 갖는다.
    public StateMachine StateMachine { get; private set; }

    // 플레이어는 상태 머신의 현재 상태를 설정하기 위해서 자신의 다양한 상태를 갖는다.
    public PlayerIdleState IdleState { get; private set;} // 정지 상태.
    public PlayerMoveState MoveState { get; private set;} // 이동 상태.

    void Awake()
    {
        StateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
    }
    void Start()
    {
        // 상태 머신 초기화.
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        // 상태 머신의 현재 상태 유지.
        StateMachine.CurrentState.Update();
    }
}
