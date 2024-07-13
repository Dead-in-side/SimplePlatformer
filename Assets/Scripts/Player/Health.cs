using System;
using UnityEngine;

public class HealthForBar : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100;

    private float _currentValue;

    public event Action IsOver;
    public event Action IsChanged;

    public float MaxValue => _maxValue;
    public float CurrentValue => _currentValue;

    private void Awake()
    {
        _currentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        if ((damage >= 0))
        {
            _currentValue -= damage;
            IsChanged?.Invoke();
        }

        if (_currentValue <= 0)
        {
            IsOver?.Invoke();
        }
    }

    public void Heal(float healPower)
    {
        if (healPower >= 0)
        {
            _currentValue += healPower;
            IsChanged?.Invoke();
        }
    }
}
