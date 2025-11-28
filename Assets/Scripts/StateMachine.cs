public class StateMachine
{
    // 상태 머신은 단 하나의 현재 상태를 갖는다.
    public EntityState CurrentState { get; private set; }

    // 상태 머신의 현재 상태를 초기화한다.
    public void Initialize(EntityState initialState)
    {
        CurrentState = initialState;
        CurrentState.Enter(); // 초기 상태 시작.
    }

    // 상태 머신의 현재 상태를 변경한다.
    public void ChangeState(EntityState newState)
    {
        CurrentState.Exit(); // 변경 전 상태 종료.
        CurrentState = newState; // 상태 변경.
        CurrentState.Enter(); // 변경 후 상태 시작.
    }
}
