using UnityEditor.Tilemaps;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Fireball : MonoBehaviour
{
    private float _speed = 3;
    private float _direction = 1;
    private float _lifeTime = 5f;
    private float _damage = 25;

    private void Update()
    {
        Move();

        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.x += _direction * _speed * Time.deltaTime;
        transform.position = position;
    }

    public void Initialise(float rotationY)
    {
        if (rotationY == 0)
        {

        }
        else
        {
            _direction = -_direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out _) && collision.TryGetComponent(out Health enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);

            Destroy(gameObject);
        }
        else if (collision.TryGetComponent<Ground>(out _))
        {
            Destroy(gameObject);
        }
    }
}
