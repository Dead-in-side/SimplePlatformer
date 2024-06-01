using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader), typeof(TriggerReader))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private InputReader _inputReader;
    private Rigidbody2D _rigidbody2D;
    private TriggerReader _triggerReader;
    private float _direction;
    private bool _isJump;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
        _triggerReader = GetComponent<TriggerReader>();
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

        if (_isJump && _triggerReader.IsGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _isJump = false;
        }
    }

    private void ChangeDirection(float newDirection) => _direction = newDirection;

    private void Jump()
    {
        if (_triggerReader.IsGrounded)
        {
            _isJump = true;
        }
    }
}
