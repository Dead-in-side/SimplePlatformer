using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PursuitArea _pursuitArea;

    [field: SerializeField] public float Damage { get; private set; } = 30f;

    private EnemyMover _mover;

    private void Awake()
    {
        _mover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _pursuitArea.PlayerEnter += _mover.Pursuit;
        _pursuitArea.PlayerExit += _mover.Patrol;
    }

    private void OnDisable()
    {
        _pursuitArea.PlayerEnter -= _mover.Pursuit;
        _pursuitArea.PlayerExit -= _mover.Patrol;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
