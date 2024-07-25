using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PursuitArea : MonoBehaviour
{
    private Collider2D _collider;

    public event Action<Player> PlayerEnter;
    public event Action PlayerExit;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out TriggerReader player))
        {
            PlayerEnter?.Invoke(player.GetComponentInParent<Player>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TriggerReader>(out _) && gameObject.activeInHierarchy)
        {
            PlayerExit?.Invoke();
        }
    }
}

