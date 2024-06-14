using System;
using UnityEngine;

public class TriggerReader : MonoBehaviour
{
    public event Action CoinIsGets;
    public event Action<Enemy> CollisionWithEnemy;
    public event Action<FirstAidKit> FirstAidKitGet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            CoinIsGets?.Invoke();

            coin.Desable();
        }
        else if (collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            FirstAidKitGet?.Invoke(firstAidKit);

            firstAidKit.Desable();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            CollisionWithEnemy?.Invoke(enemy);
        }
    }
}
