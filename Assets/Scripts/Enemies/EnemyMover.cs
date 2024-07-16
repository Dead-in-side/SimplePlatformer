using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _targetArray;
    [SerializeField] private Rotator _rotator;

    private Rigidbody2D _rigidbody;
    private Coroutine _moverCoroutine;
    private float _minDistance = 1f;
    private int _index;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _index = 0;

        _target = _targetArray[_index];

        _moverCoroutine = StartCoroutine(PatrulCoroutine());
    }

    public void Pursuit(Player player)
    {
        StopCoroutine(_moverCoroutine);

        _moverCoroutine = StartCoroutine(MoveToPlayer(player));
    }

    public void Patrol()
    {
        StopCoroutine(_moverCoroutine);

        _moverCoroutine = StartCoroutine(PatrulCoroutine());
    }

    private IEnumerator PatrulCoroutine()
    {
        bool isWork = true;

        Vector3 distance;
        Vector3 direction;

        while (isWork)
        {
            _rigidbody.velocity = new Vector2(_target.position.normalized.x * _speed, _rigidbody.velocity.y);

            distance = _target.position - transform.position;

            distance.y = 0;

            if (distance.magnitude <= _minDistance)
            {
                _index = ++_index % _targetArray.Length;

                _target = _targetArray[_index];

                direction = (_target.position - transform.position).normalized;

                _rotator.ChangeDirection(direction.x);
            }

            yield return null;
        }
    }

    private IEnumerator MoveToPlayer(Player player)
    {
        bool isWork = true;

        Vector3 target;
        Vector3 direction;

        while (isWork)
        {
            target = player.transform.position;

            direction = (target - transform.position).normalized;

            _rigidbody.velocity = new Vector2(direction.x * _speed, _rigidbody.velocity.y);

            _rotator.ChangeDirection(direction.x);

            yield return null;
        }
    }
}