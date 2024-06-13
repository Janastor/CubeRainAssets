using UnityEngine.Events;

public interface ISpawner
{
    event UnityAction StatsChanged;
    
    int SpawnedCount { get; }
    int ActiveCount { get; }
}
