using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Jump = "Jump";

    public event Action<float> MoveButtonPressed;
    public event Action JumpButonPressed;
    public event Action ZeroMouseButtomPressed;

    public float Direction {  get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        MoveButtonPressed?.Invoke(Direction);

        if (Input.GetButtonDown(Jump))
        {
            JumpButonPressed?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            ZeroMouseButtomPressed?.Invoke();
        }
    }
}