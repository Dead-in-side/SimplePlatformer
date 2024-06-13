using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private float _direction;
    private bool _isJump;
    private float _angleRotate = 180f;
    private Quaternion _startRotation;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector2(_direction * _speed * Time.fixedDeltaTime, _rigidbody2D.velocity.y);

        if (_isJump && _isGrounded)
        {
            _rigidbody2D.AddForce(Vector2.up * _jumpForce);
            _isJump = false;
        }
    }

    public void ChangeDirection(float newDirection)
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

    public void ChangeFulcrum(bool isGrounded)=>_isGrounded = isGrounded;

    public void Jump()
    {
        if (_isGrounded)
        {
            _isJump = true;
        }
    }
}
