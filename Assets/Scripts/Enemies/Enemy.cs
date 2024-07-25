using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Collider2D), typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PursuitArea _pursuitArea;

    private EnemyMover _mover;
    [field: SerializeField] public float Damage { get; private set; } = 30f;
    public Health Health {  get; private set; }

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _pursuitArea.PlayerEnter += _mover.Pursuit;
        _pursuitArea.PlayerExit += _mover.Patrol;

        Health.IsOver += Die;
    }

    private void OnDisable()
    {
        _pursuitArea.PlayerEnter -= _mover.Pursuit;
        _pursuitArea.PlayerExit -= _mover.Patrol;

        Health.IsOver -= Die;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
