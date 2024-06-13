using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerInfoDisplay : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerMono;
    [SerializeField] private string _descriptionText;

    private ISpawner _spawner;
    private string _spawnedText = "Spawned: ";
    private string _activeText = "Active: ";
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _spawner = _spawnerMono as ISpawner;

        if (_spawner != null)
        {
            _spawner.StatsChanged += UpdateText;
            UpdateText();
        }
    }
    
    private void UpdateText()
    {
        _text.text = _descriptionText + "\n" +
                     _spawnedText + _spawner.SpawnedCount + "\n" +
                     _activeText + _spawner.ActiveCount;
    }
}
