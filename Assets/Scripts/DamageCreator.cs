using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class DamageCreator : MonoBehaviour
{
    [SerializeField] private HealthForBar _health;

    private Button _button;
    private float _damage = 20;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(MakeDamage);
    }

    private void MakeDamage()=>_health.TakeDamage(_damage);
}
