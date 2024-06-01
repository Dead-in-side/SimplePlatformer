using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    public event Action<float> MoveButtonPressed;
    public event Action JumpButonPressed;

    private void Update()
    {
        float direction = Input.GetAxis(Horizontal);
        MoveButtonPressed?.Invoke(direction);

        if (Input.GetAxis(Jump) > 0)
        {
            JumpButonPressed?.Invoke();
        }
    }
}