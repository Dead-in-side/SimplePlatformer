using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    private float _healthRegeneration = 30;
    public event Action HealthEnd;

    public void TakeDamage(Enemy enemy)
    {
        _health -= enemy.Damage;

        Debug.Log(_health);

        if (_health <= 0)
        {
            HealthEnd?.Invoke();
        }
    }

    public void Heal()
    {
        _health += _healthRegeneration;

        Debug.Log(_health);
    }
}
