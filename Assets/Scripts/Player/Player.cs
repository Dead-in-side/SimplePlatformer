using UnityEngine;

[RequireComponent(typeof(Mover), typeof(TriggerReader), typeof(Wallet))]
[RequireComponent (typeof(Health), typeof(Vampirism))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Legs _legs;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private Fireball _fireball;
    [SerializeField] private AinmationShifter _animationShifter;
    [SerializeField] private VampirismView _vampirismeView;

    private Mover _mover;
    private TriggerReader _triggerReader;
    private Wallet _wallet;
    private Health _health;
    private Vampirism _vampirism;

    private void Awake()
    {
        _wallet = GetComponent<Wallet>();
        _triggerReader = GetComponent<TriggerReader>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
        _vampirism = GetComponent<Vampirism>();
    }

    private void OnEnable()
    {
        _legs.GroundedIsChanged += _mover.ChangeFulcrum;
        _legs.GroundedIsChanged += _animationShifter.ChangeFulcrum;

        _inputReader.MoveButtonPressed += _mover.ChangeDirection;
        _inputReader.MoveButtonPressed += _rotator.ChangeDirection;
        _inputReader.MoveButtonPressed += _animationShifter.ChangeAnimationDirection;
        _inputReader.JumpButonPressed += _mover.Jump;
        _inputReader.ZeroMouseButtomPressed += Attack;
        _inputReader.VampirismButtonPressed += _vampirism.Switch;
        _inputReader.VampirismButtonPressed += _vampirism.PullOutHealth;
        _inputReader.VampirismButtonPressed += _vampirismeView.Play;

        _triggerReader.CoinTaken += _wallet.AddCoin;
        _triggerReader.CollisionWithEnemyHappened += _health.TakeDamage;
        _triggerReader.FirstAidKitTaken += _health.Heal;

        _health.IsOver += _animationShifter.Die;

        _vampirism.ReceivedHealth += _health.Heal;
    }

    private void OnDisable()
    {
        _legs.GroundedIsChanged -= _mover.ChangeFulcrum;
        _legs.GroundedIsChanged -= _animationShifter.ChangeFulcrum;

        _inputReader.MoveButtonPressed -= _mover.ChangeDirection;
        _inputReader.MoveButtonPressed -= _rotator.ChangeDirection;
        _inputReader.MoveButtonPressed -= _animationShifter.ChangeAnimationDirection;
        _inputReader.JumpButonPressed -= _mover.Jump;
        _inputReader.ZeroMouseButtomPressed -= Attack;
        _inputReader.VampirismButtonPressed -= _vampirism.Switch;
        _inputReader.VampirismButtonPressed -= _vampirism.PullOutHealth;
        _inputReader.VampirismButtonPressed -= _vampirismeView.Play;

        _triggerReader.CoinTaken -= _wallet.AddCoin;
        _triggerReader.CollisionWithEnemyHappened -= _health.TakeDamage;
        _triggerReader.FirstAidKitTaken -= _health.Heal;

        _health.IsOver -= _animationShifter.Die;

        _vampirism.ReceivedHealth -= _health.Heal;
    }

    private void Attack()
    {
        Fireball fireball = Instantiate(_fireball, transform.position, transform.rotation);
        fireball.Initialise(_rotator.transform.rotation.y);
    }
}
