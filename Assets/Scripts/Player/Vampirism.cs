using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismArea _area;
    [SerializeField] private VampirismView _view;

    private float _maxDuration = 6f;
    private float _duration = 6f;
    private float _absorptionSpeed = 5f;
    private Enemy _enemyTarget;
    private bool _isAvailable = true;

    public event Action<float> ReceivedHealth;
    public event Action DurationChanged;

    public float MaxDuration => _maxDuration;
    public float CurrentDuration => _duration;

    private void OnEnable()
    {
        _area.NearestEnemyGeted += SetNearestEnemy;
    }

    private void OnDisable()
    {
        _area.NearestEnemyGeted -= SetNearestEnemy;
    }

    public void PullOutHealth()
    {
        if (_isAvailable)
        {
            _isAvailable = false;

            _area.Play();

            _view.Play();

            StartCoroutine(LifeTransferCoroutine());
        }
    }

    private IEnumerator LifeTransferCoroutine()
    {
        float passedHealth;

        while (_duration > 0)
        {
            passedHealth = _absorptionSpeed * Time.deltaTime;

            _duration -= Time.deltaTime;
            DurationChanged?.Invoke();

            if (_enemyTarget != null)
            {
                _enemyTarget.Health.TakeDamage(passedHealth);

                ReceivedHealth?.Invoke(passedHealth);
            }

            yield return null;
        }

        _view.Stop();

        _area.Stop();

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        while (_duration < _maxDuration)
        {
            _duration += Time.deltaTime;
            DurationChanged?.Invoke();

            yield return null;
        }

        _isAvailable = true;
    }

    private void SetNearestEnemy(Enemy enemy) => _enemyTarget = enemy;
}
