using UnityEngine;

[RequireComponent (typeof(TriggerReader))]

public class Wallet : MonoBehaviour
{
    private TriggerReader _triggerReader;

    public int CoinNumber { get; private set; }

    private void Awake()
    {
        _triggerReader = GetComponent<TriggerReader>();

        CoinNumber = 0;
    }

    private void OnEnable()
    {
        _triggerReader.CoinIsGets += AddCoin;
    }

    private void OnDisable()
    {
        _triggerReader.CoinIsGets -= AddCoin;
    }

    private void AddCoin()=>CoinNumber++;
}
