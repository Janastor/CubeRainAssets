using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerInfoDisplay : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private string _descriptionText;
    
    private string _spawnedText = "Spawned: {0}";
    private string _activeText = "Active: {0}";
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    private void Update()
    {
        _text.text = _descriptionText + "\n" +
                     string.Format(_spawnedText, _spawner.SpawnedCount) +"\n"+
                     string.Format(_activeText, _spawner.ActiveCount);
    }
}
