using DevilsDen.Events;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cam;
    private CharacterController controller;
    public float velocity = 10f;
    public float rotateTime = 0.1f;
    public float dashVelocity = 40f;
    private float dashCooldown = 2.0f;
    private float dashDoubleTapTime = 0.2f;
    private bool isDashing = false;
    private float _rotateVelocity;
    private KeyCode _lastDashKey;
    private Vector3 _dashDirection;
    private float _lastDashKeyTime;
    private float _lastDashTime = -2f;
    private int currentDashes = 0;
    private bool isDashIntent
    {
        get
        {
            return (Time.time - _lastDashKeyTime) < dashDoubleTapTime;
        }
    }
    private bool canMove
    {
        get
        {
            return (Time.time - _lastDashTime) > dashCooldown;
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 moveVector = HandleDash();
        if (moveVector == Vector3.zero)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            moveVector = new Vector3(x, 0, z).normalized;
            if (moveVector.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _rotateVelocity, rotateTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                moveVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * velocity;
            }
        }
        moveVector.y = 0f;
        controller.Move(moveVector * Time.deltaTime);
    }

    Vector3 HandleDash()
    {
        if (isDashing) return BuildDashVector();
        if (canMove)
        {
            if (InputManager.instance.GetKeyDown(InputManager.Forward))
            {
                if (isDashIntent && _lastDashKey == KeyCode.W)
                {
                    _dashDirection = BuildDashDirection(cam.forward);
                    return BuildDashVector();
                }
                _lastDashKey = KeyCode.W;
                _lastDashKeyTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (isDashIntent && _lastDashKey == KeyCode.A)
                {
                    _dashDirection = BuildDashDirection(-cam.right);
                    return BuildDashVector();
                }
                _lastDashKey = KeyCode.A;
                _lastDashKeyTime = Time.time;

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (isDashIntent && _lastDashKey == KeyCode.S)
                {
                    _dashDirection = BuildDashDirection(-cam.forward);
                    return BuildDashVector();
                }
                _lastDashKey = KeyCode.S;
                _lastDashKeyTime = Time.time;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (isDashIntent && _lastDashKey == KeyCode.D)
                {
                    _dashDirection = BuildDashDirection(cam.right);
                    return BuildDashVector();
                }
                _lastDashKey = KeyCode.D;
                _lastDashKeyTime = Time.time;
            }
        }
        return Vector3.zero;
    }

    Vector3 BuildDashDirection(Vector3 direction)
    {
        currentDashes++;
        var dir = direction;
        dir.y = 0;
        return dir;
    }

    Vector3 BuildDashVector()
    {
        isDashing = true;
        _lastDashTime = Time.time;
        if (Time.time - _lastDashKeyTime >= 1f) isDashing = false;
        if (Time.time - _lastDashKeyTime <= 0.5f)
        {
            var x = Mathf.Pow((Time.time + 0.5f - _lastDashKeyTime), 8);
            var timeVariable = -x + 1;
            return controller.velocity * timeVariable;
        }
        else
        {
            float x = Mathf.Pow(((Time.time - _lastDashKeyTime)), 4);
            var timeVariable = -x + 1;
            return _dashDirection * dashVelocity * timeVariable;
        }
    }
}
