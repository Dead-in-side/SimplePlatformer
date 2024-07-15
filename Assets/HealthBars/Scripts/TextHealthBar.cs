using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
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

    private void UpdateValue()=>_textMeshPro.text = _health.CurrentValue + " // " + _health.MaxValue;
}
