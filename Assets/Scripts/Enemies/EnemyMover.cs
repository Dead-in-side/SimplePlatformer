using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _currentTime;

    private float _rotationAngle = 180;
    private Queue<Vector2> _targetConteiner = new();
    private Rigidbody2D _rigidbody;
    private Coroutine _moverCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _targetConteiner.Enqueue(Vector2.right);
        _targetConteiner.Enqueue(Vector2.left);

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

        float time = 0f;

        while (isWork)
        {
            time += Time.deltaTime;

            _rigidbody.velocity = new Vector2(_targetConteiner.Peek().x * _speed, _rigidbody.velocity.y);

            if (time >= _currentTime)
            {
                _targetConteiner.Enqueue(_targetConteiner.Dequeue());

                time = 0f;

                if (_targetConteiner.Peek().x < 0)
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