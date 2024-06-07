using System;
using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _bombPrefab;
    
    public ObjectPool<Bomb> Pool { get; private set; }
    
    private void Awake()
    {
        Pool = new ObjectPool<Bomb>
        (
            createFunc: () => Instantiate(_bombPrefab, transform),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: _poolCapacity,
            maxSize: _poolSize
        );
    }

    private void Update()
    {
        ActiveCount = Pool.CountActive;
    }

    public Bomb GetBomb()
    {
        SpawnedCount++;
        return Pool.Get();
    }
}
