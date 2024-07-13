using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HealCreator : MonoBehaviour
{
    [SerializeField] private HealthForBar _health;

    private Button _button;
    private float _healPower = 20;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(MakeHeal);
    }

    private void MakeHeal() => _health.Heal(_healPower);
}
