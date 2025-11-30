using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    // 플레이어를 위해 생성한 입력 시스템(InputActions)를 갖는다.
    public PlayerInputActions InputActions { get; private set; }

    public Vector2 MoveDirection { get; private set; } // 플레이어의 이동 방향.
    public float moveSpeed; // 플레이어의 이동 속력.
    public float jumpForce; // 플레이어의 점프 힘(Y축, 수직축).
    public float wallJumpForce; // 플레이어의 벽점프 힘(X축, 수평축).
    [Range(0, 1)]
    public float wallSlideFallMultiplier = 0.3f; // 플레이어의 벽타기 낙하 계수.
    private bool facingRight = true; // 플레이어가 바라보는 방향.
    public int FacingDirection { get; private set; } = 1; // 플레이어가 바라보는 방향(+1, -1).

    // 플레이어는 자신의 상태 머신을 갖는다.
    private StateMachine stateMachine;

    // 플레이어는 상태 머신의 현재 상태를 설정하기 위해서 자신의 다양한 상태를 갖는다.
    public PlayerIdleState IdleState { get; private set;} // 정지 상태.
    public PlayerMoveState MoveState { get; private set;} // 이동 상태.
    public PlayerJumpState JumpState { get; private set; } // 점프 상태.
    public PlayerFallState FallState { get; private set; } // 낙하 상태.
    public PlayerWallSlideState WallSlideState { get; private set; } // 벽타기 상태.
    public PlayerWallJumpState WallJumpState { get; private set; } // 벽점프 상태.

    [SerializeField] private float distanceToGround = 1.5f;
    [SerializeField] private LayerMask groundLayer;
    public bool OnGround { get; private set; } = true;

    [SerializeField] private float distanceToWall = 0.4f;
    public bool OnWall { get; private set; } = true;

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        InputActions = new PlayerInputActions();

        stateMachine = new StateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        JumpState = new PlayerJumpState(this, stateMachine, "air");
        FallState = new PlayerFallState(this, stateMachine, "air");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, "air");
    }

    void OnEnable()
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

    void Start()
    {
        // 상태 머신 초기화.
        stateMachine.Initialize(IdleState);
    }

    void Update()
    {
        // 상태 머신의 현재 상태 유지.
        stateMachine.Play();

        RaycastGround();
    }

    void OnDisable()
    {
        InputActions.Disable(); // 입력 시스템 비활성화.
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceToGround);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * distanceToWall * FacingDirection);
    }

    // 플레이어의 이동을 결정한다.
    public void Move(float xVelocity, float yVelocity)
    {
        Rigidbody.linearVelocity = new Vector2(xVelocity, yVelocity);

        if ((xVelocity > 0 && !facingRight) || (xVelocity < 0 && facingRight))
        {
            Flip();
        }
    }

    // 플레이어 이미지를 좌우반전한다.
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        FacingDirection = -FacingDirection;
    }

    private void RaycastGround()
    {
        OnGround = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, groundLayer);
        OnWall = Physics2D.Raycast(transform.position, Vector2.right * FacingDirection, distanceToWall, groundLayer);
    }
}
