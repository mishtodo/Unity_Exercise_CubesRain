using UnityEngine;
using TMPro;

public class CountsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _spawnedCounts;
    [SerializeField] private TextMeshProUGUI _createdCounts;
    [SerializeField] private TextMeshProUGUI _activeCubesCounts;
    [SerializeField] private TextMeshProUGUI _activeBombCounts;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _bombSpawner.MetersChanged += UpdateDisplay;
        _cubeSpawner.MetersChanged += UpdateDisplay;

        UpdateDisplay();
    }

    private void OnDisable()
    {
        _bombSpawner.MetersChanged -= UpdateDisplay;
        _cubeSpawner.MetersChanged -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        _spawnedCounts.text = (_bombSpawner.TotalSpawned + _cubeSpawner.TotalSpawned).ToString();
        _createdCounts.text = (_bombSpawner.TotalCreated + _cubeSpawner.TotalCreated).ToString();
        _activeCubesCounts.text = _cubeSpawner.ActiveCount.ToString();
        _activeBombCounts.text = _bombSpawner.ActiveCount.ToString();
    }
}