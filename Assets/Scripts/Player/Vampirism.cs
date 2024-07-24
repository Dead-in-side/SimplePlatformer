using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismArea _area;
    [SerializeField] private VampirismView _view;

    private float _maxDuration = 6f;
    private float _duration = 6f;
    private float _absorptionSpeed = 5f;
    private Enemy _enemyTarget;
    private bool _isEnable = true;

    public event Action<float> ReceivedHealth;
    public event Action IsChanged;

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
        if (_isEnable)
        {
            _isEnable = false;

            _area.gameObject.SetActive(true);

            _view.Play();

            StartCoroutine(LifeTransferCoroutine());
        }
    }

    private IEnumerator LifeTransferCoroutine()
    {
        float passedHealth = _absorptionSpeed * Time.deltaTime;

        while (_duration > 0)
        {
            _duration -= Time.deltaTime;
            IsChanged?.Invoke();


            if (_enemyTarget != null)
            {
                _enemyTarget.Health.TakeDamage(passedHealth);

                ReceivedHealth?.Invoke(passedHealth);
            }

            yield return null;
        }

        _area.gameObject.SetActive(false);

        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        while (_duration < _maxDuration)
        {
            _duration += Time.deltaTime;
            IsChanged?.Invoke();

            yield return null;
        }

        _isEnable = true;
    }

    private void SetNearestEnemy(Enemy enemy) => _enemyTarget = enemy;
}
