using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        UpdateValue();
    }

    private void OnEnable()
    {
        _health.IsChanged += UpdateValue;
    }

    private void OnDisable()
    {
        _health.IsChanged -= UpdateValue;
    }

    private void UpdateValue() => _slider.value = _health.CurrentValue / _health.MaxValue;
}
