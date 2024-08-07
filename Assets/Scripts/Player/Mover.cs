using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _rigidbody2D;
    private float _direction;
    private bool _isJump;
    private bool _isGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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
