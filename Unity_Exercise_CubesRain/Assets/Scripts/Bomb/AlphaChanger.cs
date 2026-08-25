using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Bomb))]
public class AlphaChanger : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Bomb _bomb;

    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        //_bomb.Hitted += ChangeColor;
    }

    private void OnDisable()
    {
        //_bomb.Hitted -= ChangeColor;
        _renderer.material.color = _defaultColor;
    }

    private void ChangeAlpha()
    {

    }
}