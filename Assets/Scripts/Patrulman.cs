using UnityEngine;

public class Patrulman : MonoBehaviour
{
    [SerializeField] private Transform _targetConteiner;
    [SerializeField] private float _speed;

    private Transform[] _targets;
    private Transform _target;
    private int _index = 0;

    private void Awake()
    {
        _targets = new Transform[_targetConteiner.childCount];

        for (int i = 0; i < _targets.Length; i++)
        {
            _targets[i] = _targetConteiner.GetChild(i);
        }

        _target = _targets[_index];
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed*Time.fixedDeltaTime);

        if(transform.position == _target.position)
        {
            ChooseNextTarget();
        }
    }

    private void ChooseNextTarget()
    {
        _index = ++_index % _targets.Length;

        _target = _targets[_index];
    }
}
