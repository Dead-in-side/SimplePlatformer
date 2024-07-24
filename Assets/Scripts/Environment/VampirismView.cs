using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class VampirismView : MonoBehaviour
{
    [SerializeField] private VampirismArea _area;

    private Vector3 _startScale;
    private Color _startColor;
    private float _maxTransparancy = 0.5f;
    private float _delay = 6f;
    private SpriteRenderer _spriteRenderer;
    private float _duration = 1f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.one;
        _startColor = _spriteRenderer.color;
    }

    public void Play()
    {
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        float time = 0;
        Color newColor = _spriteRenderer.color;

        float sizeFactor = _area.Range * _area.Range * Time.deltaTime / _duration;
        float colorFactor = _maxTransparancy * Time.deltaTime / _duration;

        while (time < _duration)
        {
            time += Time.deltaTime;

            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * _area.Range * _area.Range, sizeFactor);

            newColor.a = Mathf.MoveTowards(newColor.a, _maxTransparancy, colorFactor);

            _spriteRenderer.color = newColor;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(_delay);

        transform.localScale = Vector3.one;
        _spriteRenderer.color = _startColor;
    }
}
