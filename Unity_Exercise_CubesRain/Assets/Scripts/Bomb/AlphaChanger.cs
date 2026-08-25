using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AlphaChanger : MonoBehaviour
{
    [SerializeField] private Material _opaqueMaterial;
    [SerializeField] private Material _fadeMaterial;

    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
        SetRenderModeToFade();
    }

    private void OnEnable()
    {
        //ChangeAlpha(_defaultColor.a);
    }

    private void OnDisable()
    {
        SetRenderModeToOpaque();
        //ChangeAlpha(_defaultColor.a);
    }

    public void ChangeAlpha(float alpha)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    public void SetRenderModeToFade()
    {
        _renderer.material = _fadeMaterial;
    }

    public void SetRenderModeToOpaque()
    {
        _renderer.material = _opaqueMaterial;
    }
}