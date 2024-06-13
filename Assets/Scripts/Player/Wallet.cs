using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> MoneyIsChanged;

    public int CoinNumber { get; private set; }

    private void Awake()
    {
        CoinNumber = 0;
    }

    public void AddCoin()
    {
        MoneyIsChanged?.Invoke(++CoinNumber);
    }
}
