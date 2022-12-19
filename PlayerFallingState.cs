using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private Vector3 momentum;

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.CharacterController.isGrounded)
        {
            stateMachine.SwitchState(new PlayerLandingState(stateMachine));
            return;
        }
        Move(momentum, deltaTime);
    }

    public override void Exit()
    {
    }
}