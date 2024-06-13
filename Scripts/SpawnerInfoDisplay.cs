using TMPro;
using UnityEngine;

public class SpawnerInfoDisplay : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawner;
    [SerializeField] private string _descriptionText;

    private ISpawner _spawnerInterface;
    private string _spawnedText = "Spawned: ";
    private string _activeText = "Active: ";
    private TMP_Text _text;
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _spawnerInterface = _spawner as ISpawner;

        if (_spawnerInterface != null)
        {
            _spawnerInterface.StatsChanged += UpdateText;
            UpdateText();
        }
    }
    
    private void UpdateText()
    {
        _text.text = _descriptionText + "\n" +
                     _spawnedText + _spawnerInterface.SpawnedCount + "\n" +
                     _activeText + _spawnerInterface.ActiveCount;
    }
}
