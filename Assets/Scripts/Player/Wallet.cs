using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action<int> MoneyQuantityChanged;

    public int CoinNumber { get; private set; }

    private void Awake()
    {
        CoinNumber = 0;
    }

    public void AddCoin()
    {
        CoinNumber++;

        MoneyQuantityChanged?.Invoke(CoinNumber);
    }
}
