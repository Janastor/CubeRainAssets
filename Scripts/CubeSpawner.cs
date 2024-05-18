using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Vector3 _spawnAreaSize;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private float _spawnDelay = 1f;

    private float _minRotation = 0;
    private float _maxRotation = 360f;
    private float _minAngularVelocity = -2f;
    private float _maxAngularVelocity = 2f;
    private bool _isWorking = true;
    private WaitForSeconds _delay;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
        (
            createFunc: () => Instantiate(_prefab, transform),
            actionOnGet: (obj) => OnCubeGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: _poolCapacity,
            maxSize: _poolSize
        );
    }

    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnCubes());
    }

    private void SpawnCube()
    {
        Cube spawned = _pool.Get();

        spawned.Init(_pool);
    }

    private void OnCubeGet(Cube cube)
    {
        cube.Rigidbody.velocity = Vector3.zero;
        cube.transform.position = GetRandomSpawnPoint();
        cube.transform.rotation = Quaternion.Euler(GetRandomRotation(_minRotation, _maxRotation));
        cube.Rigidbody.angularVelocity = GetRandomRotation(_minAngularVelocity, _maxAngularVelocity);
        cube.Init(_pool);
        cube.gameObject.SetActive(true);
    }

    private Vector3 GetRandomRotation(float min, float max)
    {
        return new Vector3
        (
            Random.Range(min, max),
            Random.Range(min, max),
            Random.Range(min, max)
        );
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int divider = 2;
        
        Vector3 offset = new Vector3
        (
            Random.Range(-_spawnAreaSize.x / divider, _spawnAreaSize.x / divider),
            Random.Range(-_spawnAreaSize.y / divider, _spawnAreaSize.y / divider),
            Random.Range(-_spawnAreaSize.z / divider, _spawnAreaSize.z / divider)
        );

        return (transform.position + offset);
    }
    
    private IEnumerator SpawnCubes()
    {
        while (_isWorking)
        {
            SpawnCube();
            yield return _delay;
        }
    }
}