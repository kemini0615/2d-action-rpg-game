public abstract class PlayerState : BaseState
{
    protected Player player;

    public PlayerState(Player player, StateMachine stateMachine, string stateName) : base(stateMachine, stateName)
    {
        this.player = player;
        this.Animator = player.Animator;
        this.Rigidbody = player.Rigidbody;
    }

    public override void Update()
    {
        base.Update();

        // Dash 상태로 트랜지션.
        if (player.InputActions.Player.Dash.WasPressedThisFrame())
        {
            if (!player.OnWall && (stateMachine.CurrentState != player.DashState))
                stateMachine.ChangeState(player.DashState);
        }
    }

    public override void UpdateAnimatorParameter()
    {
        base.UpdateAnimatorParameter();

        Animator.SetFloat("yVelocity", Rigidbody.linearVelocity.y);
    }
}
