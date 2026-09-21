using UnityEngine;

public class PlayerThrowState : PlayerState
{
    private float throwDuration = 0.35f;
    private float timer;
    private bool hasThrown;

    public PlayerThrowState(StateMachine sm, int abn, Player p)
        : base(sm, abn, p)
    {
    }

    public override void EnterState()
    {
        timer = 0f;
        hasThrown = false;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;

        // PLAYER MOVEMENT WHILE THROWING
        if (player.MoveInput.x != 0)
        {
            player.SetVelocity(
                player.MoveSpeed * player.MoveInput.x,
                rb.linearVelocityY
            );
        }
        else
        {
            // Stop horizontal movement if there is no input.
            player.SetVelocity(
                0f,
                rb.linearVelocityY
            );
        }

        // THROW PIZZA
        if (!hasThrown && timer >= throwDuration * 0.35f)
        {
            hasThrown = true;
            player.ThrowPizza();
        }

        // FINISH THROW
        if (timer >= throwDuration)
        {
            if (!player.GroundDetected)
            {
                stateMachine.ChangeState(player.FallState);
            }
            else if (player.MoveInput.x != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else
            {
                stateMachine.ChangeState(player.IdleState);
            }
        }
    }

    public override void ExitState()
    {
    }

    public override void UpdateAnimationParameter()
    {
    }
}