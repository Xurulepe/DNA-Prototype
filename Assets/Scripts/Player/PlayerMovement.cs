using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Vector3 _moveInput;

    [Header("Dash Settings")]
    [SerializeField] private float _dashSpeed = 20f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] private float _dashCooldownTimer = 0f;
    [SerializeField] private bool _canDash = true;
    [SerializeField] private bool _isDashing = false;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_canDash)
        {
            DashCooldown();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    #region MOVE
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector3>().normalized;
    }

    private void Move()
    {
        if (!_isDashing)
        {
            _rigidbody.linearVelocity = _moveInput * _moveSpeed;
        }
    }
    #endregion

    #region DASH
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && _canDash)
        {
            Dash();
        }
    }

    private void Dash()
    {
        _isDashing = true;
        _rigidbody.linearVelocity = _moveInput * _dashSpeed;
        _canDash = false;
    }

    private void DashCooldown()
    {
        _dashCooldownTimer += Time.deltaTime;

        if (_dashCooldownTimer >= _dashCooldown)
        {
            _isDashing = false;
            _canDash = true;
            _dashCooldownTimer = 0f;
        }
    }
    #endregion
}
