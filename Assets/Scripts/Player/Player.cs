using UnityEngine;

[RequireComponent(typeof(Mover), typeof(TriggerReader), typeof(Wallet))]
[RequireComponent (typeof(AinmationShifter), typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Legs _legs;
    [SerializeField] private Fireball _fireball;

    private Mover _mover;
    private TriggerReader _triggerReader;
    private Wallet _wallet;
    private AinmationShifter _animationShifter;
    private Health _health;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _triggerReader = GetComponent<TriggerReader>();
        _mover = GetComponent<Mover>();
        _animationShifter = GetComponent<AinmationShifter>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _legs.IsGroundedChanged += _mover.ChangeFulcrum;
        _legs.IsGroundedChanged += _animationShifter.ChangeFulcrum;

        _inputReader.MoveButtonPressed += _mover.ChangeDirection;
        _inputReader.MoveButtonPressed += _animationShifter.ChangeAnimationDirection;
        _inputReader.JumpButonPressed += _mover.Jump;
        _inputReader.MouseButtomPressed += Attack;

        _triggerReader.CoinIsGets += _wallet.AddCoin;
        _triggerReader.CollisionWithEnemy += _health.TakeDamage;
        _triggerReader.FirstAidKitGet += _health.Heal;

        _health.HealthEnd += _animationShifter.Die;
    }

    private void OnDisable()
    {
        _legs.IsGroundedChanged -= _mover.ChangeFulcrum;
        _legs.IsGroundedChanged -= _animationShifter.ChangeFulcrum;

        _inputReader.MoveButtonPressed -= _mover.ChangeDirection;
        _inputReader.MoveButtonPressed -= _animationShifter.ChangeAnimationDirection;
        _inputReader.JumpButonPressed -= _mover.Jump;
        _inputReader.MouseButtomPressed -= Attack;

        _triggerReader.CoinIsGets -= _wallet.AddCoin;
        _triggerReader.CollisionWithEnemy -= _health.TakeDamage;
        _triggerReader.FirstAidKitGet -= _health.Heal;

        _health.HealthEnd -= _animationShifter.Die;
    }

    private void Attack()
    {
        Fireball fireball = Instantiate(_fireball, transform.position, transform.rotation);
    }
}
