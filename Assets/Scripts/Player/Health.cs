using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    public event Action IsOver;

    public void TakeDamage(Enemy enemy)
    {
        if ((enemy.Damage >= 0))
        {
            _health -= enemy.Damage;
        }

        Debug.Log(_health);

        if (_health <= 0)
        {
            IsOver?.Invoke();
        }
    }

    public void Heal(FirstAidKit firstAidKit)
    {
        if (firstAidKit.HealPower >= 0)
        {
            _health += firstAidKit.HealPower;
        }

        Debug.Log(_health);
    }
}
