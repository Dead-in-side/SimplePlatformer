using System;
using UnityEngine;

public class TriggerReader : MonoBehaviour
{
    public event Action CoinTaken;
    public event Action<Enemy> CollisionWithEnemyHappened;
    public event Action<FirstAidKit> FirstAidKitTaken;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            CoinTaken?.Invoke();

            coin.Desable();
        }
        else if (collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            FirstAidKitTaken?.Invoke(firstAidKit);

            firstAidKit.Desable();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            CollisionWithEnemyHappened?.Invoke(enemy);
        }
    }
}
