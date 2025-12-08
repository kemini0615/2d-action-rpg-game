public abstract class PlayerState : EntityState
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

        player.Animator.SetFloat("yVelocity", Rigidbody.linearVelocity.y);

        // Dash 상태로 트랜지션.
        if (player.InputActions.Player.Dash.WasPressedThisFrame())
        {
            if (!player.OnWall && (stateMachine.CurrentState != player.DashState))
                stateMachine.ChangeState(player.DashState);
        }
    }
}
