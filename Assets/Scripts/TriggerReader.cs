using System;
using UnityEngine;

public class TriggerReader : MonoBehaviour
{
    public event Action CoinIsGets;
    public event Action CollisionWithEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            CoinIsGets?.Invoke();

            coin.Desable();
        }
        else if (collision.gameObject.TryGetComponent<Enemy>(out _))
        {
            CollisionWithEnemy?.Invoke();
        }
    }
}
