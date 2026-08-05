using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Material), typeof(Rigidbody))]
public class Bomb : SpawnableObject
{
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _explosionForce = 850f;

    private Renderer _renderer;
    private Material _material;
    private Color _initialColor;
    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        _initialColor = _material.color;
    }

    private void OnEnable()
    {
        _material.color = _initialColor;
        SetRenderModeToOpaque();
        RestartCoroutine();
    }

    public void StopSpawnCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void RestartCoroutine()
    {
        _coroutine = StartCoroutine(LiveAndExplode());
    }

    private IEnumerator LiveAndExplode()
    {
        float duration = Random.Range(2f, 5f);
        float elapsed = 0f;

        SetRenderModeToFade();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            Color color = _material.color;
            color.a = alpha;
            _material.color = color;

            yield return null;
        }

        Explode();

        NotifyDying();
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private void SetRenderModeToFade()
    {
        _material.SetOverrideTag("RenderType", "Transparent");
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        _material.SetInt("_ZWrite", 0);
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.EnableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    private void SetRenderModeToOpaque()
    {
        _material.SetOverrideTag("RenderType", "");
        _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        _material.SetInt("_ZWrite", 1);
        _material.DisableKeyword("_ALPHATEST_ON");
        _material.DisableKeyword("_ALPHABLEND_ON");
        _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        _material.renderQueue = -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
