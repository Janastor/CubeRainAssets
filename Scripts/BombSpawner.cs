using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private float _spawnDelay = 1f;
    
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
    
    public Bomb GetBomb()
    {
        Bomb bomb = Pool.Get();
        return bomb;
    }
}
