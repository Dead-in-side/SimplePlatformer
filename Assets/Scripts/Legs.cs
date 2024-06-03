using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Legs : MonoBehaviour
{
    public Collider2D Collider2D { get; private set; }

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            IsGrounded = false;
        }
    }
}
