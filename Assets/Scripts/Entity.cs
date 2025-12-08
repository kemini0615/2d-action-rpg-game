using UnityEngine;

public class Entity : MonoBehaviour
{
    protected StateMachine stateMachine;

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    private bool facingRight = true; // 개체가 바라보는 방향.
    public int FacingDirection { get; private set; } = 1; // 개체가 바라보는 방향(+1, -1).
    
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float distanceToGround = 1.5f;
    [SerializeField] private LayerMask groundLayer;
    public bool OnGround { get; private set; } = true;

    [SerializeField] private float distanceToWall = 0.4f;
    [SerializeField] private Transform highWallChecker;
    [SerializeField] private Transform lowWallChecker;
    public bool OnWall { get; private set; } = true;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();

        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start() {}

    protected virtual void Update()
    {
        // 상태 머신의 현재 상태 유지.
        stateMachine.Play();

        Raycast();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundChecker.position, groundChecker.position + Vector3.down * distanceToGround);
        Gizmos.DrawLine(highWallChecker.position, highWallChecker.position + Vector3.right * distanceToWall * FacingDirection);
        Gizmos.DrawLine(lowWallChecker.position, lowWallChecker.position + Vector3.right * distanceToWall * FacingDirection);
    }

    // 플레이어의 이동을 결정한다.
    public void Move(float xVelocity, float yVelocity)
    {
        Rigidbody.linearVelocity = new Vector2(xVelocity, yVelocity);

        if ((xVelocity > 0 && !facingRight) || (xVelocity < 0 && facingRight))
            Flip();
    }

    // 플레이어 이미지를 좌우반전한다.
    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        FacingDirection = -FacingDirection;
    }

    private void Raycast()
    {
        OnGround = Physics2D.Raycast(groundChecker.position, Vector2.down, distanceToGround, groundLayer);
        OnWall = Physics2D.Raycast(highWallChecker.position, Vector2.right * FacingDirection, distanceToWall, groundLayer)
                    && Physics2D.Raycast(lowWallChecker.position, Vector2.right * FacingDirection, distanceToWall, groundLayer);
    }
}
