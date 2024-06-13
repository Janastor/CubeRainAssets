using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Bomb : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    private float _explosionTimer;
    private float _explosionRadius = 4f;
    private float _explosionForce = 5f;
    private float _minExplosionTime = 2f;
    private float _maxExplosionTime = 5f;
    private float _alpha = 1f;
    private BombSpawner _spawner;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 position, Vector3 velocicy, BombSpawner spawner)
    {
        transform.position = position;
        _rigidbody.velocity = velocicy;
        _spawner = spawner;
        StartCoroutine(ExplosionSequence());
    }

    private void AddExplosionForce()
    {
        List<Collider> colliders = Physics.OverlapSphere(transform.position, _explosionRadius).ToList();
        
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddForce(CalculateDirection(rigidbody.position) * _explosionForce, ForceMode.Impulse);
        }
    }

    private Vector3 CalculateDirection(Vector3 position)
    {
        return (position - transform.position).normalized;
    }

    private IEnumerator ExplosionSequence()
    {
        _explosionTimer = Random.Range(_minExplosionTime, _maxExplosionTime);
        float normalizedTimer = 1f;

        while (normalizedTimer > 0)
        {
            _alpha = normalizedTimer;
            normalizedTimer -= (1 / _explosionTimer) * Time.deltaTime;
            _meshRenderer.material.color = new Color(_meshRenderer.material.color.r, _meshRenderer.material.color.g, _meshRenderer.material.color.b, _alpha);
            yield return null;
        }
        
        AddExplosionForce();
        _spawner.Despawn(this);
    }
}
