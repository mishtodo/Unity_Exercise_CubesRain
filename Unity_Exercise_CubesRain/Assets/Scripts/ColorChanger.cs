using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Cube))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Cube _cube;

    private Color _defaultColor;
    private Color _randomColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        _cube.Hitted += ChangeColor;
    }

    private void OnDisable()
    {
        _cube.Hitted -= ChangeColor;
        _renderer.material.color = _defaultColor;
    }

    private void ChangeColor()
    {
        _randomColor = new(Random.value, Random.value, Random.value);
        _renderer.material.color = _randomColor;
    }
}