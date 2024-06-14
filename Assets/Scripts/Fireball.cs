using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Fireball : MonoBehaviour
{
    private float _speed = 3;
    private Rigidbody2D _rigidbody;
    private float _lifeTime = 5f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.right * _speed;

        _lifeTime-=Time.fixedDeltaTime;
        if(_lifeTime <= 0)
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
}
