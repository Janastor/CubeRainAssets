using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour, ISpawner where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    [SerializeField] protected int _poolCapacity = 10;
    [SerializeField] protected int _poolSize = 10;
    
    protected ObjectPool<T> _pool;
    private int _spawnedCount = 0;
    private int _activeCount = 0;

    public event UnityAction StatsChanged;
    
    public int SpawnedCount => _spawnedCount;
    public int ActiveCount => _activeCount;
    
    private void Awake()
    {
        _pool = new ObjectPool<T>
        (
            createFunc: () => Instantiate(_prefab, transform),
            actionOnGet: (obj) => OnObjectGet(obj),
            actionOnRelease: (obj) => OnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: _poolCapacity,
            maxSize: _poolSize
        );
    }
    
    public void Despawn(T item)
    {
        _pool.Release(item);
    }

    protected T Spawn()
    {
        return _pool.Get();
    }

    private void OnRelease(T item)
    {
        item.gameObject.SetActive(false);
        _activeCount = _pool.CountActive;
        StatsChanged?.Invoke();
    }

    private void OnObjectGet(T item)
    {
        item.gameObject.SetActive(true);
        _activeCount = _pool.CountActive;
        _spawnedCount++;
        StatsChanged?.Invoke();
    }
}