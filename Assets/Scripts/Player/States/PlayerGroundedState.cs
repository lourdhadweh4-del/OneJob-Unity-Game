using UnityEngine;

public class PlayerGroundedState : PlayerState {
    public PlayerGroundedState(StateMachine sm, int abn, Player p) : base(sm, abn, p) {
    }

    public override void EnterState() {
        base.EnterState();
    }
    public override void UpdateState() {
        base.UpdateState();


        if (rb.linearVelocityY < 0 && !player.GroundDetected)
            stateMachine.ChangeState(player.FallState);

        if (inputSet.Player.Jump.WasPressedThisFrame() && player.GroundDetected)
            stateMachine.ChangeState(player.JumpState);

        if (inputSet.Player.Slip.WasPressedThisFrame())
            player.Slip();

    }

    public override void ExitState() {
        base.ExitState();
    }

}
