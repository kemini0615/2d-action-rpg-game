using System.Collections;
using UnityEngine;

public class Player : Entity
{
    // 플레이어를 위해 생성한 입력 시스템(InputActions)를 갖는다.
    public PlayerInputActions InputActions { get; private set; }

    // 플레이어는 상태 머신의 현재 상태를 설정하기 위해서 자신의 다양한 상태를 갖는다.
    public PlayerIdleState IdleState { get; private set;} // 정지 상태.
    public PlayerMoveState MoveState { get; private set;} // 이동 상태.
    public PlayerJumpState JumpState { get; private set; } // 점프 상태.
    public PlayerFallState FallState { get; private set; } // 낙하 상태.
    public PlayerWallSlideState WallSlideState { get; private set; } // 벽타기 상태.
    public PlayerWallJumpState WallJumpState { get; private set; } // 벽점프 상태.
    public PlayerDashState DashState { get; private set; } // 대시 상태.
    public PlayerAttackState AttackState { get; private set; } // 공격 상태.
    public PlayerJumpAttackState JumpAttackState { get; private set; } // 점프 공격 상태.

    [field: SerializeField] public Vector2 MoveDirection { get; private set; } = Vector2.zero; // 플레이어의 이동 방향.
    [field: SerializeField] public float MoveSpeed { get; private set; } = 10f; // 플레이어의 이동 속력.
    [field: SerializeField] public float DashSpeed { get; private set; } = 30f; // 플레이어 대시 속력.
    [field: SerializeField] public float DashDuration { get; private set; } = 0.2f; // 플레이어 대시 시간.
    [field: SerializeField] public float JumpForce { get; private set; } = 15f; // 플레이어의 점프 힘(Y축, 수직축).
    [field: SerializeField] public float WallJumpForce { get; private set; } = 15f; // 플레이어의 벽점프 힘(X축, 수평축).
    [Range(0, 1)][field: SerializeField] public float WallSlideFallMultiplier { get; private set; } = 0.3f; // 플레이어의 벽타기 낙하 계수.

    [field: SerializeField] public float AttackMovingSpeed { get; private set; } = 5f; // 플레이어가 공격하면서 움직이는 속력.
    [field: SerializeField] public float AttackMovingDuration { get; private set; } = 0.1f; // 플레이어가 공격하면서 움직일 수 있는 시간.
    [field: SerializeField] public Vector2 JumpAttackMovingVelocity { get; private set; } = new Vector2(5f, -5f); // 플레이어가 점프 공격하면서 움직이는 속도.
    [field: SerializeField] public float JumpAttackMovingDuration { get; private set; } = 0.1f; // 플레이어가 점프 공격하면서 움직일 수 있는 시간.
    [field: SerializeField] public float ComboDuration { get; private set; } = 2f; // 콤보 가능 시간.

    // 코루틴 함수를 실행하면 반환되는 객체를 참조한다.
    private Coroutine comboAttackCoroutine;

    protected override void Awake()
    {
        base.Awake();

        InputActions = new PlayerInputActions();

        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        JumpState = new PlayerJumpState(this, stateMachine, "air");
        FallState = new PlayerFallState(this, stateMachine, "air");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, "air");
        DashState = new PlayerDashState(this, stateMachine, "dash");
        AttackState = new PlayerAttackState(this, stateMachine, "attack");
        JumpAttackState = new PlayerJumpAttackState(this, stateMachine, "jumpAttack");
    }

    protected override void Start()
    {
        base.Start();

        // 상태 머신 초기화.
        stateMachine.Initialize(IdleState);
    }

    // 코루틴 함수.
    private IEnumerator Co_ComboAttack()
    {
        yield return new WaitForEndOfFrame(); // 현재 프레임이 끝날 때까지 기다린다.
        stateMachine.ChangeState(AttackState);
    }

    // 컴포넌트(Monobehaviour 클래스)가 아니면 코루틴을 실행할 수 없기 때문에 래핑한다.
    public void ComboAttack()
    {
        if (comboAttackCoroutine != null)
            StopCoroutine(comboAttackCoroutine);

        comboAttackCoroutine = StartCoroutine(Co_ComboAttack());
    }

    private void OnEnable()
    {
        InputActions.Enable(); // 입력 시스템 활성화.

        // Player: 액션 맵 이름.
        // Move: 액션 이름.
        // started: 입력 시작 이벤트.
        // performed: 입력 유지 이벤트.
        // canceled: 입력 종료 이벤트.
        InputActions.Player.Move.performed += context => MoveDirection = context.ReadValue<Vector2>(); // started 이벤트에 구독하면 대각선 이동이 제대로 구현되지 않는다.
        InputActions.Player.Move.canceled += context => MoveDirection = Vector2.zero;
    }

    private void OnDisable()
    {
        InputActions.Disable(); // 입력 시스템 비활성화.
    }

}
