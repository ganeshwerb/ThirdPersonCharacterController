using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int fallingHash = Animator.StringToHash("Falling");
    private Vector3 momentum = new(0, 0, 0);

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
        if (stateMachine.PlayerInputRegister.MovementValue == Vector2.zero)
        {
            momentum = Vector3.zero;
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);
        if (stateMachine.CharacterController.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        stateMachine.Animator.CrossFadeInFixedTime(fallingHash, 0.35f);
    }

    public override void Exit()
    {
    }
}