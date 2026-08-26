using UnityEngine;
using TMPro;

public class ActiveCountView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _count;
    [SerializeField] private BaseSpawner _spawner;

    private void OnEnable()
    {
        if (_spawner != null)
        {
            _spawner.MetersChanged += UpdateText;
            UpdateText();
        }
    }

    private void OnDisable()
    {
        if (_spawner != null)
        {
            _spawner.MetersChanged -= UpdateText;
        }
    }

    private void UpdateText()
    {
        _count.text = _spawner.ActiveCount.ToString();
    }
}