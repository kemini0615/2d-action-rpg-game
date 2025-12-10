using UnityEngine;

public abstract class EntityState
{
    [field:SerializeField] protected Rigidbody2D Rigidbody { get; set; }
    [field:SerializeField] protected Animator Animator { get; set; }

    protected StateMachine stateMachine; // 모든 상태는 상태 머신을 참조한다.
    protected string stateName; // 애니메이션의 트랜지션을 위한 파라미터로 사용된다.
    protected float timer; // 모든 상태는 타이머를 갖는다.
    public bool AnimationEventTriggered { get; private set; } = false;

    public EntityState(StateMachine stateMachine, string stateName)
    {
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    // 상태가 시작될 때 호출된다.
    public virtual void Enter()
    {
        Animator.SetBool(this.stateName, true); // 애니메이션 재생 시작.

        SetAnimationEventTriggered(false); // 애니메이션 이벤트 발생 플래그 초기화.
    }

    // 상태가 유지될 때 호출된다.
    public virtual void Update()
    {
        timer -= Time.deltaTime; // 타이머 시간 감소.
        UpdateAnimatorParameter(); // 애니메이터 파라미터 갱신.
    }

    // 상태가 종료될 때 호출된다.
    public virtual void Exit()
    {
        Animator.SetBool(this.stateName, false); // 애니메이션 재생 종료.
    }

    public void SetAnimationEventTriggered(bool value)
    {
        AnimationEventTriggered = value;
    }

    public virtual void UpdateAnimatorParameter() {}
}
