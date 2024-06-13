using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Fireball : MonoBehaviour
{
    private float _speed = 3;
    private float _distance = 15;
    private Vector2 _target;
    private int _direction = 0;

    private void Start()
    {
        _target = new Vector2(transform.position.x + _distance * _direction, transform.position.y);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

        if (transform.position.x == _target.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();

            Destroy(gameObject);
        }
        else if (collision.TryGetComponent<Ground>(out _))
        {
            Destroy(gameObject);
        }
    }

    public void ChangeDirection(float rotation)
    {
        if (rotation < 0)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }
    }
}
