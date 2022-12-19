using UnityEngine;

public class PlayerAimingState : PlayerBaseState
{
    public PlayerAimingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    private const float crossfadeDuration = 0.15f;

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("AimIdle", crossfadeDuration);
    }

    public override void Exit()
    {
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.PlayerInputRegister.SeeIsAiming())
        {
            stateMachine.SwitchState(new PlayerFreelookState(stateMachine));
            return;
        }
        stateMachine.transform.rotation = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f);
        //Ray toward screen center
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit1, 500f))
        {
            Debug.DrawRay(Camera.main.transform.position, (hit1.point - Camera.main.transform.position) * (Camera.main.transform.position - hit1.point).magnitude, Color.white, 0.2f);
            //if ray1 hit enemy
            if (hit1.transform.gameObject.CompareTag("Enemy"))
            {
                //shoot ray2 from enemy to origin
                if (Physics.Raycast(hit1.point, (stateMachine.projectileOrigin.position - hit1.point) * (stateMachine.projectileOrigin.position - hit1.point).magnitude, out RaycastHit hit2))
                {
                    Debug.DrawRay(hit1.point, (stateMachine.projectileOrigin.position - hit1.point) * (stateMachine.projectileOrigin.position - hit1.point).magnitude, Color.red, 0.2f);

                    if (hit2.transform.CompareTag("Player"))
                    {
                        stateMachine.aimReticle.color = Color.red;
                    }
                    else
                    {
                        stateMachine.aimReticle.color = Color.blue;
                    }
                }
            }

            else
            {
                stateMachine.aimReticle.color = Color.white;
            }
        }
        if (stateMachine.PlayerInputRegister.SeeIsAttacking())
        {
            stateMachine.UpdateTarget(hit1.point);
            stateMachine.Animator.CrossFadeInFixedTime("Throw", crossfadeDuration);
        }
    }
}