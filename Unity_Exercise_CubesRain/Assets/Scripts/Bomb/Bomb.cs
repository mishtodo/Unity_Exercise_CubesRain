using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Material), typeof(Rigidbody))]
public class Bomb : SpawnableObject
{
    [SerializeField] private float _explosionRadius = 50f;
    [SerializeField] private float _explosionForce = 850f;
    [SerializeField] private Material _opaqueMaterial;
    [SerializeField] private Material _fadeMaterial;

    private Renderer _renderer;
    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
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

            Color color = _renderer.material.color;
            color.a = alpha;
            _fadeMaterial.color = color;

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
        _renderer.material = _fadeMaterial;
    }

    private void SetRenderModeToOpaque()
    {
        _renderer.material = _opaqueMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
