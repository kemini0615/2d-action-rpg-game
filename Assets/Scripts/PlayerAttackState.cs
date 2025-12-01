using UnityEngine;

public class PlayerAttackState : EntityState
{
    private const int ComboCount = 3;
    private int comboIndex = 0;
    private float lastAttackTime = 0f;
    private bool nextComboAttackQueued = false;
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
        nextComboAttackQueued = false; // 연속 콤보 공격 플래그 초기화.
        
        // 공격 방향 설정.
        if (player.MoveDirection.x != 0)
        {
            attackDirection = (int)player.MoveDirection.x;
        }
        else
        {
            attackDirection = player.FacingDirection;
        }

        timer = player.MovingAttackDuration; // 플레이어가 움직이면서 공격하는 시간 설정.
    }

    public override void Update()
    {
        base.Update();

        if (timer >= 0)
        {
            // 설정한 공격 방향으로 움직이면서 공격.
            player.Move(player.movingAttackSpeed * attackDirection, player.Rigidbody.linearVelocity.y);
        }
        else
        {
            // 정지.
            player.Move(0f, player.Rigidbody.linearVelocity.y);
        }

        // 세번째 콤보 공격 이후에는 연속 콤보 공격 불가능.
        if (player.InputActions.Player.Attack.WasPressedThisFrame() && comboIndex < ComboCount - 1)
        {
            nextComboAttackQueued = true;
        }

        if (AnimationEventTriggered)
        {
            if (nextComboAttackQueued)
            {
                // 연속 콤보 공격: 다음 Attack 상태로 트랜지션.
                player.Animator.SetBool(stateName, false);
                player.ComboAttack(); // 프레임이 끝난 다음에 Attack 상태로 트랜지션.
            }
            else
            {
                // Idle 상태로 트랜지션.
                stateMachine.ChangeState(player.IdleState);
            }

            SetAnimationEventTriggered(false);
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;
        lastAttackTime = Time.time;
    }
}
