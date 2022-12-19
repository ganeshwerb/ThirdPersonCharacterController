using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    private readonly int landing_2 = Animator.StringToHash("Landing 2");
    private const float crossFadeDuration = 0.15f;

    public PlayerLandingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(landing_2, crossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).IsName("Landing 2") && stateMachine.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}