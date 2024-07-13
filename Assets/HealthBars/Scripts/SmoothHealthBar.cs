using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private HealthForBar _health;

    private Slider _slider;
    private float _speed= 0.001F;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _health.IsChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _health.IsChanged -= UpdateValue;
    }

    private void UpdateValue()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(UpdateValueCoroutine());
    }

    private IEnumerator UpdateValueCoroutine()
    {
        float target = _health.CurrentValue / _health.MaxValue;

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, _speed);

            yield return null;
        }
    }
}
