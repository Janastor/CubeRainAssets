using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = System.Object;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected int _poolCapacity = 10;
    [SerializeField] protected int _poolSize = 10;
    
    public int SpawnedCount { get; protected set; }
    public int ActiveCount { get; protected set; }
}
