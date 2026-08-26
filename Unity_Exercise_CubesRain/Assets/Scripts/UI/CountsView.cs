using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalSpawnedCounts;
    [SerializeField] private TextMeshProUGUI _totalCreatedCounts;
    [SerializeField] private List<BaseSpawner> _spawners;

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
        {
            if (spawner != null) spawner.MetersChanged += UpdateDisplay;
        }

        UpdateDisplay();
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            if (spawner != null) spawner.MetersChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay()
    {
        int totalSpawned = 0;
        int totalCreated = 0;

        foreach (var spawner in _spawners)
        {
            if (spawner != null)
            {
                totalSpawned += spawner.TotalSpawned;
                totalCreated += spawner.TotalCreated;
            }
        }

        _totalSpawnedCounts.text = totalSpawned.ToString();
        _totalCreatedCounts.text = totalCreated.ToString();
    }
}