using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float _angleRotate = 180f;
    private Quaternion _startRotation;
    private float _direction;

    private void Start()
    {
        _startRotation = transform.rotation;
    }

    public void ChangeDirection(float direction)
    {
        if (direction < 0)
        {
            transform.rotation = Quaternion.AngleAxis(_angleRotate, Vector2.up);
        }
        else if (direction > 0)
        {
            transform.rotation = _startRotation;
        }
    }
}
