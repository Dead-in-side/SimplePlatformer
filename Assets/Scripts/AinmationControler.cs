using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputReader), typeof(TriggerReader))]

public class AinmationControler : MonoBehaviour
{
    [SerializeField] private Legs _legs;

    private TriggerReader _triggerReader;
    private Animator _animator;
    private InputReader _inputReader;
    private string _nameIsRun = "IsRun";
    private string _nameIsJump = "Jump";
    private string _nameIsDie = "Dead";
    private float _direction;
    private bool _isDead = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputReader = GetComponent<InputReader>();
        _triggerReader = GetComponent<TriggerReader>();
    }

    private void Update()
    {
        _animator.SetBool(_nameIsRun, _direction != 0);
        _animator.SetBool(_nameIsJump, !_legs.IsGrounded);
        _animator.SetBool(_nameIsDie, _isDead);
    }

    private void OnEnable()
    {
        _inputReader.MoveButtonPressed += ChangeDirection;
        _triggerReader.CollisionWithEnemy += Die;
    }

    private void OnDisable()
    {
        _inputReader.MoveButtonPressed -= ChangeDirection;
        _triggerReader.CollisionWithEnemy -= Die;
    }

    private void Die()=>_isDead = true;

    private void ChangeDirection(float direction) => _direction = direction;
}
