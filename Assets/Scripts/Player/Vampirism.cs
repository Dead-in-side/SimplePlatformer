using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VampirismArea))]
public class Vampirism : MonoBehaviour
{
    private float _maxDuration = 6f;
    private float _duration = 6f;
    private float _changingSpeed = 1f;
    private float _absorptionSpeed = 2f;
    private VampirismArea _area;
    private List<Enemy> _enemyList = new List<Enemy>();
    private bool _isEnable = false;

    public event Action<float> ReceivedHealth;
    public event Action IsChanged;

    public float MaxDuration => _maxDuration;
    public float CurrentDuration => _duration;

    private void Awake()
    {
        _area = GetComponent<VampirismArea>();
    }

    private void Update()
    {
        if (!_isEnable && _duration < _maxDuration)
        {
            _duration += _changingSpeed * Time.deltaTime;

            IsChanged?.Invoke();
        }
        else if (_isEnable && _duration > 0)
        {
            _duration -= _changingSpeed * Time.deltaTime;

            IsChanged?.Invoke();

            if (_duration <= 0)
            {
                _isEnable = false;
            }
        }
    }

    public void PullOutHealth()
    {
        if (_isEnable)
        {
            _enemyList = _area.GetEnemies();

            if (_enemyList.Count > 0)
            {
                foreach (Enemy enemy in _enemyList)
                {
                    if (enemy.TryGetComponent(out Health health))
                    {
                        StartCoroutine(LifeTransferCoroutine(health));
                    }
                }
            }
            else
            {
                _isEnable = false;
            }
        }
    }

    public void Switch()=> _isEnable = !_isEnable;

    private IEnumerator LifeTransferCoroutine(Health health)
    {
        float passedHealth;

        while (_isEnable && _duration > 0 && health.CurrentValue > 0)
        {
            passedHealth = _absorptionSpeed * Time.deltaTime;

            health.TakeDamage(passedHealth);

            ReceivedHealth?.Invoke(passedHealth);

            IsChanged?.Invoke();

            yield return null;
        }
    }
}
