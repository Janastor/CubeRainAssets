using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerInfoDisplay : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private string _descriptionText;
    
    private string _spawnedText = "Spawned: ";
    private string _activeText = "Active: ";
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    private void Update()
    {
        _text.text = _descriptionText + "\n" +
                     _spawnedText + _spawner.SpawnedCount + "\n" +
                     _activeText + _spawner.ActiveCount;
    }
}
