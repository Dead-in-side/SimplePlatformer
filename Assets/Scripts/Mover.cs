using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]

public class Mover : MonoBehaviour
{
    [SerializeField] private Legs _legs;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private InputReader _inputReader;
    private Rigidbody2D _rigidbody2D;
    private float _direction;
    private bool _isJump;
    private float _angleRotate = 180f;
    private Quaternion _startRotation;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
    }

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        _inputReader.MoveButtonPressed += ChangeDirection;
        _inputReader.JumpButonPressed += Jump;
    }

    private void OnDisable()
    {
        _inputReader.MoveButtonPressed -= ChangeDirection;
        _inputReader.JumpButonPressed -= Jump;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_direction * _speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);

        if (_isJump && _legs.IsGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _isJump = false;
        }
    }

    private void ChangeDirection(float newDirection)
    {
        _direction = newDirection;

        if (_direction < 0)
        {
            transform.rotation = Quaternion.AngleAxis(_angleRotate, Vector2.up);
        }
        else if (_direction > 0)
        {
            transform.rotation = _startRotation;
        }
    }

    private void Jump()
    {
        if (_legs.IsGrounded)
        {
            _isJump = true;
        }
    }
}
