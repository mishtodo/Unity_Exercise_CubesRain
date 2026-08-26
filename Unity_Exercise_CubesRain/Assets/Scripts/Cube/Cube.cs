using System;
using System.Collections;
using UnityEngine;

public class Cube : SpawnableObject
{
    private Coroutine _coroutine;

    public bool HaveHitted { get; private set; }
    public event Action Hitted;

    protected override void OnEnable()
    {
        base.InitializeVelocity(Vector3.zero);
        HaveHitted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HaveHitted == false && collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            HaveHitted = true;
            Hitted?.Invoke();
            RestartCoroutine();
        }
    }

    public void StopSpawnCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void RestartCoroutine()
    {
        _coroutine = StartCoroutine(WaitBeforeDestroyCube());
    }

    private IEnumerator WaitBeforeDestroyCube()
    {
        float minDestroyDelay = 2;
        float maxDestroyDelay = 5;
        float RandomDelay = UnityEngine.Random.Range(minDestroyDelay, maxDestroyDelay);
        var wait = new WaitForSecondsRealtime(RandomDelay);

        yield return wait;
        NotifyDying();
    }
}