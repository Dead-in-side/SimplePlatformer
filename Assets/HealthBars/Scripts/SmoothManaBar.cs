using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmoothManaBar : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;

    private Slider _slider;
    private float _speed = 0.001F;
    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _vampirism.DurationChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _vampirism.DurationChanged -= UpdateValue;
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
        float target = _vampirism.CurrentDuration / _vampirism.MaxDuration;

        while (_slider.value != target)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, target, _speed);

            yield return null;
        }
    }
}
