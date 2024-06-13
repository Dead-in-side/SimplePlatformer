using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AinmationShifter : MonoBehaviour
{
    private readonly int IsRun = Animator.StringToHash(nameof(IsRun));
    private readonly int IsJump = Animator.StringToHash(nameof(IsJump));
    private readonly int IsDie = Animator.StringToHash(nameof(IsDie));

    private Animator _animator;
    private float _direction;
    private bool _isDead = false;
    private bool _isGrounded;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool(IsRun, _direction != 0 && _isGrounded);
        _animator.SetBool(IsJump, !_isGrounded);
        _animator.SetBool(IsDie, _isDead);
    }

    public void Die()=>_isDead = true;

    public void ChangeAnimationDirection(float direction) => _direction = direction;

    public void ChangeFulcrum(bool isGrounded) => _isGrounded = isGrounded;
}
