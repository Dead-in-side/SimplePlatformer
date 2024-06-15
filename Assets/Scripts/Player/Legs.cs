using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Legs : MonoBehaviour
{
    private bool _isGrounded;

    public event Action<bool> GroundedIsChanged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = true;

            GroundedIsChanged?.Invoke(_isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ground>(out _))
        {
            _isGrounded = false;

            GroundedIsChanged?.Invoke(_isGrounded);
        }
    }
}
