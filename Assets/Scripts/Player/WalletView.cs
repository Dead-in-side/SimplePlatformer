using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private TextMeshProUGUI _textMeshProUGUI;
    private int _money = 0;

    private void Awake()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _textMeshProUGUI.text = "Money: " + _money;
    }

    private void OnEnable()
    {
        _wallet.MoneyQuantityChanged += ShowMoney;
    }

    private void OnDisable()
    {
        _wallet.MoneyQuantityChanged -= ShowMoney;
    }

    private void ShowMoney(int money)
    {
        _money = money;

        _textMeshProUGUI.text = "Money: " + _money;
    }
}
