using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    private Vector3 _moveInput;

    [Header("Dash Settings")]
    [SerializeField] private float _dashPower = 20f;
    [SerializeField] private float _dashDuration = 0.2f;
    private float _dashDurationTimer = 0f;
    [SerializeField] private float _dashCooldown = 1f;
    private float _dashCooldownTimer = 0f;
    private bool _canDash = true;
    private bool _isDashing = false;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_canDash && !_isDashing)
        {
            DashCooldown();
        }

        if (_isDashing)
        {
            DashController();
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
            _rigidbody.linearVelocity = new Vector3(_moveInput.x * _moveSpeed, _rigidbody.linearVelocity.y, _moveInput.z * _moveSpeed);
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
        _rigidbody.AddForce(_moveInput * _dashPower, ForceMode.VelocityChange);
        _canDash = false;
    }

    private void DashController()
    {
        _dashDurationTimer += Time.deltaTime;

        if (_dashDurationTimer >= _dashDuration)
        {
            _isDashing = false;
            _dashDurationTimer = 0f;
        }
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
