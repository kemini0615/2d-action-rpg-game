using UnityEngine;

public class PlayerAttackState : EntityState
{
    private const int ComboCount = 3;
    private int comboIndex = 0;
    private float lastAttackTime = 0f;
    private bool nextComboAttackPressed = false;
    private int attackDirection;

    public PlayerAttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName) {}

    public override void Enter()
    {
        base.Enter();
        
        comboIndex %= ComboCount;
        
        // 콤보 가능 시간이 지나면 첫번째 콤보 공격으로 리셋.
        if (lastAttackTime + player.ComboDuration < Time.time)
        {
            comboIndex = 0;
        }

        player.Animator.SetInteger("comboIndex", comboIndex); // 콤보 공격 애니메이션 설정.
        nextComboAttackPressed = false; // 연속 콤보 공격 플래그 초기화.
        
        attackDirection = player.MoveDirection.x != 0 ? (int)player.MoveDirection.x : player.FacingDirection; // 공격 방향 설정.

        timer = player.AttackMovingDuration; // 플레이어가 움직이면서 공격하는 시간 설정.
    }

    public override void Update()
    {
        base.Update();

        if (timer >= 0)
            player.Move(player.AttackMovingSpeed * attackDirection, player.Rigidbody.linearVelocity.y); // 설정한 공격 방향으로 움직이면서 공격.
        else
            player.Move(0f, player.Rigidbody.linearVelocity.y); // 정지.

        // 세번째 콤보 공격 이후에는 연속 콤보 공격 불가능.
        if (player.InputActions.Player.Attack.WasPressedThisFrame() && comboIndex < ComboCount - 1)
            nextComboAttackPressed = true;

        if (AnimationEventTriggered)
        {
            HandleNextAttackState();
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastAttackTime = Time.time;
    }

    private void HandleNextAttackState()
    {
        if (nextComboAttackPressed)
        {
            player.Animator.SetBool(stateName, false);
            player.ComboAttack(); // 프레임이 끝난 다음에 Attack 상태로 트랜지션.
        }
        else
        {
            stateMachine.ChangeState(player.IdleState); // Idle 상태로 트랜지션.
        }

        SetAnimationEventTriggered(false);
    }
}
