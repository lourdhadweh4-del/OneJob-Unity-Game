using UnityEngine;

public abstract class PlayerState : EntityState {
    protected Player player;
    protected PlayerInputSet inputSet;

    protected bool canDash = true;

    private static readonly int _yVelocityHash = Animator.StringToHash("yVelocity");

    public PlayerState(StateMachine sm, int abn, Player p) : base(sm, abn) {
        player = p;

        anim = player.Anim;
        rb = player.RB;
        inputSet = player.InputSet;
        // stats = player.Stats;
    }


    // EnterState gets called at first after transitioning to a new state.
    public override void EnterState() {
        base.EnterState();
    }


    // UpdateState gets called each frame and runs the logic of the current state
    public override void UpdateState() {

        base.UpdateState();
    }


    // ExitState of the current state gets called before transitioning to a new state.
    public override void ExitState() {
        base.ExitState();
    }


    // For updating the JumpFall blend tree based on the player's current vertical velocity
    // Gets called in the update method from entity state
    public override void UpdateAnimationParameter() {
        base.UpdateAnimationParameter();

        anim.SetFloat(_yVelocityHash, rb.linearVelocityY);
    }
}
