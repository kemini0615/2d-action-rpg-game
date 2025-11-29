using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어를 위해 생성한 입력 시스템(InputActions)를 갖는다.
    private PlayerInputActions inputActions;

    public Vector2 MoveDirection { get; private set; } // 플레이어의 이동 방향.

    // 플레이어는 자신의 상태 머신을 갖는다.
    private StateMachine stateMachine;

    // 플레이어는 상태 머신의 현재 상태를 설정하기 위해서 자신의 다양한 상태를 갖는다.
    public PlayerIdleState IdleState { get; private set;} // 정지 상태.
    public PlayerMoveState MoveState { get; private set;} // 이동 상태.

    public Animator Animator { get; private set; }

    void Awake()
    {
        inputActions = new PlayerInputActions();
        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        Animator = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        inputActions.Enable(); // 입력 시스템 활성화.

        // Player: 액션 맵 이름.
        // Move: 액션 이름.
        // started: 입력 시작 이벤트.
        // performed: 입력 유지 이벤트.
        // canceled: 입력 종료 이벤트.
        inputActions.Player.Move.performed += context => MoveDirection = context.ReadValue<Vector2>(); // started 이벤트에 구독하면 대각선 이동이 제대로 구현되지 않는다.
        inputActions.Player.Move.canceled += context => MoveDirection = Vector2.zero;
    }

    void Start()
    {
        // 상태 머신 초기화.
        stateMachine.Initialize(IdleState);
    }

    void Update()
    {
        // 상태 머신의 현재 상태 유지.
        stateMachine.Play();
    }

    void OnDisable()
    {
        inputActions.Disable(); // 입력 시스템 비활성화.
    }
}
