using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _targetArray;

    private float _rotationAngle = 180;
    private Rigidbody2D _rigidbody;
    private Coroutine _moverCoroutine;
    private float _currentTime = 5f;
    private float _minDistance = 0.1f;
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

        float time = 0f;

        while (isWork)
        {
            time += Time.deltaTime;

            _rigidbody.velocity = new Vector2(_target.position.normalized.x * _speed, _rigidbody.velocity.y);

            distance = _target.position - transform.position;

            distance.y = 0;

            if (time >= _currentTime || distance.magnitude <= _minDistance)
            {
                _index = ++_index % _targetArray.Length;

                _target = _targetArray[_index];

                time = 0f;

                if ((_target.position - transform.position).normalized.x < 0)
                {
                    transform.rotation = Quaternion.AngleAxis(_rotationAngle, Vector2.up);
                }
                else
                {
                    transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
                }
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

            if (direction.x > 0)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector2.up);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(_rotationAngle, Vector2.up);
            }

            yield return null;
        }
    }
}