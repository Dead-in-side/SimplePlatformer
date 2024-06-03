using UnityEngine;

[RequireComponent (typeof(Camera))]

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private float _zPozition = -10f;

    private void Update()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, _zPozition);
    }
}
