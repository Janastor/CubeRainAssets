using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private ObjectPool<Cube> _pool;
    private float _minColorValue = 0.25f;
    private float _maxColorValue = 0.9f;
    private float _minDisappearTime = 2f;
    private float _maxDisappearTime = 5f;
    private bool _isDisappearing;
    
    public Rigidbody Rigidbody { get; private set; }
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDisappearing == false && collision.collider.TryGetComponent(out CubeDespawner _))
            StartCoroutine(DisappearSequence());
    }

    private void Colorize()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = GetRandomColor();
    }

    public void Init(ObjectPool<Cube> pool)
    {
        _pool = pool;
        _isDisappearing = false;
        Colorize();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue), Random.Range(_minColorValue, _maxColorValue));
    }

    private IEnumerator DisappearSequence()
    {
        _isDisappearing = true;
        Colorize();
        yield return new WaitForSeconds(Random.Range(_minDisappearTime, _maxDisappearTime));
        _pool.Release(this);
    }
}
