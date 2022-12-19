using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputRegister : MonoBehaviour, PlayerInput.IPlayerActions
{
    public event Action jumpEvent;

    [field: SerializeField] private bool IsSprinting;
    [field: SerializeField] private bool IsAiming;
    [field: SerializeField] private bool IsAttacking;
    [field: SerializeField] private float timePassed;
    private readonly float attackCooldown = 4f;
    [field: SerializeField] public Vector2 MovementValue { get; private set; }
    private PlayerInput inputActions;
    public PlayerInput PlayerInput
    { get { return PlayerInput; } }

    private void Awake()
    {
        inputActions = new PlayerInput();
        inputActions.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        jumpEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed && MovementValue == Vector2.zero) { IsAiming = true; }
        if (context.canceled || MovementValue != Vector2.zero) { IsAiming = false; }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public bool SeeIsAiming()
    {
        bool b = IsAiming;
        return b;
    }

    public bool SeeIsAttacking()
    {
        bool b = IsAttacking;
        return b;
    }

    public bool SeeIsSprinting()
    {
        bool b = IsSprinting;
        return b;
    }

    public void ChangeIsAiming(bool x)
    {
        IsAiming = x;
    }

    public void ChangeIsAttacking(bool x)
    {
        IsAttacking = x;
    }

    public void ChangeIsSprinting(bool x)
    {
        IsSprinting = x;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed) { IsSprinting = true; }
        if (context.canceled) { IsSprinting = false; }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (timePassed >= attackCooldown && context.performed && IsAttacking == false)
        {
            timePassed = 0f;
            IsAttacking = true;
            StartCoroutine(Dlay());
        }
    }

    private IEnumerator Dlay()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        IsAttacking = false;
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
    }
}