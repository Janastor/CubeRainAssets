using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Vector3 _spawnAreaSize;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private BombSpawner _bombSpawner;

    private float _minRotation = 0;
    private float _maxRotation = 360f;
    private float _minAngularVelocity = -2f;
    private float _maxAngularVelocity = 2f;
    private bool _isWorking = true;
    private WaitForSeconds _delay;
    
    private void Start()
    {
        _delay = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnCubes());
    }

    private void SpawnCube()
    {
        Cube cube = Spawn();
        
        cube.Rigidbody.velocity = Vector3.zero;
        cube.transform.position = GetRandomSpawnPoint();
        cube.transform.rotation = Quaternion.Euler(GetRandomRotation(_minRotation, _maxRotation));
        cube.Rigidbody.angularVelocity = GetRandomRotation(_minAngularVelocity, _maxAngularVelocity);
        cube.Init(_pool, _bombSpawner);
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