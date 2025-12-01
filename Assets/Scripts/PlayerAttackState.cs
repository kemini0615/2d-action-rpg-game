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
        
        // 콤보 공격 시간이 지나면 첫번째 공격으로 리셋.
        if (lastAttackTime + player.ComboDuration < Time.time)
        {
            comboIndex = 0;
        }

        player.Animator.SetInteger("comboIndex", comboIndex);
        nextComboAttackQueued = false;
        
        if (player.MoveDirection.x != 0)
        {
            attackDirection = (int)player.MoveDirection.x;
        }
        else
        {
            attackDirection = player.FacingDirection;
        }

        timer = player.MovingAttackDuration;
    }

    public override void Update()
    {
        base.Update();

        if (timer >= 0)
        {
            // 움직이면서 공격 가능.
            player.Move(player.movingAttackSpeed * attackDirection, player.Rigidbody.linearVelocity.y);
        }
        else
        {
            // 정지.
            player.Move(0f, player.Rigidbody.linearVelocity.y);
        }

        // 세번째 마지막 공격 이후에는 콤보 공격 불가능.
        if (player.InputActions.Player.Attack.WasPressedThisFrame() && comboIndex < ComboCount - 1)
        {
            nextComboAttackQueued = true;
        }

        if (AnimationEventTriggered)
        {
            if (nextComboAttackQueued)
            {
                // 다음 Attack 상태로 트랜지션.
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
