using Unity.VisualScripting;
using UnityEngine;

public class PlayerFallState : PlayerAirState {
    public PlayerFallState(StateMachine sm, int abn, Player p) : base(sm, abn, p) {
    }

    public override void EnterState() {
        base.EnterState();
    }
    public override void UpdateState() {
        base.UpdateState();

        if (player.GroundDetected)
            stateMachine.ChangeState(player.IdleState);

        if (player.transform.position.y < -50f)
            player.GameIsOver();
    }

    public override void ExitState() {
        base.ExitState();
    }

}
