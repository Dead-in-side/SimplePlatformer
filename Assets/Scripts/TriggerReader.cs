using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class TriggerReader : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    public event Action CoinIsGets;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground _))
        {
            IsGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out Ground _))
        {
            IsGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            CoinIsGets?.Invoke();
        }
    }
}
