using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputReader), typeof(TriggerReader))]

public class AinmationShifter : MonoBehaviour
{
    [SerializeField] private Legs _legs;

    private readonly int IsRun = Animator.StringToHash(nameof(IsRun));
    private readonly int IsJump = Animator.StringToHash(nameof(IsJump));
    private readonly int IsDie = Animator.StringToHash(nameof(IsDie));

    private TriggerReader _triggerReader;
    private Animator _animator;
    private InputReader _inputReader;
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
        _animator.SetBool(IsRun, _direction != 0);
        _animator.SetBool(IsJump, !_legs.IsGrounded);
        _animator.SetBool(IsDie, _isDead);
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
