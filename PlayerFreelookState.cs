using UnityEngine;

public class PlayerFreelookState : PlayerBaseState
{
    public PlayerFreelookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private readonly int freelookBlendtreeHash = Animator.StringToHash("FreeLookBlendtree");
    private readonly int Movement = Animator.StringToHash("MovementSpeed");
    private const float animatorDamptime = 0.15f;
    private const float crossfadeDuration = 0.35f;

    public override void Enter()
    {
        stateMachine.PlayerInputRegister.jumpEvent += OnJump;
        stateMachine.Animator.CrossFadeInFixedTime(freelookBlendtreeHash, crossfadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 moveDirection = CalculateMovement();
        if (stateMachine.PlayerInputRegister.MovementValue != Vector2.zero)
        {
            stateMachine.PlayerInputRegister.ChangeIsAiming(false);
            if (stateMachine.PlayerInputRegister.SeeIsSprinting())
            {
                stateMachine.Animator.SetFloat(Movement, 1f, animatorDamptime, deltaTime);
                Move(moveDirection.normalized * stateMachine.FreeLookSprintSpeed, deltaTime);
            }
            else
            {
                stateMachine.Animator.SetFloat(Movement, 0.5f, animatorDamptime, deltaTime);
                Move(moveDirection.normalized * stateMachine.FreeLookWalkSpeed, deltaTime);
            }
            FaceMoveDirection(moveDirection, deltaTime);
        }
        else
        {
            if (stateMachine.PlayerInputRegister.SeeIsAiming())
            {
                stateMachine.SwitchState(new PlayerAimingState(stateMachine));
            }
            else
                stateMachine.Animator.SetFloat(Movement, 0f, animatorDamptime, deltaTime);
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.PlayerInputRegister.jumpEvent -= OnJump;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 camForward = stateMachine.Cam.forward;
        Vector3 camRight = stateMachine.Cam.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        return stateMachine.PlayerInputRegister.MovementValue.x * camRight + stateMachine.PlayerInputRegister.MovementValue.y * camForward;
    }

    private void FaceMoveDirection(Vector3 moveDir, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation, Quaternion.LookRotation(moveDir), deltaTime * stateMachine.RotationDamping);
    }
}