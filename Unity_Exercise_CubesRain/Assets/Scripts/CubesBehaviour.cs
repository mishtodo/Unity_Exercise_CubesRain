using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class CubesBehaviour : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private ReturnToPool _ReturnToPool;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _ReturnToPool.Entered += ReturnCube;
    }

    private void OnDisable()
    {
        _ReturnToPool.Entered -= ReturnCube;
    }

    public void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void RestartCoroutine()
    {
        _coroutine = StartCoroutine(WaitBeforeDestroyCube());
    }

    private void ReturnCube(Cube cube)
    {
        _cubeSpawner.ActionOnRelease(cube);
    }

    private IEnumerator WaitBeforeDestroyCube()
    {
        var wait = new WaitForSecondsRealtime(1.0f);

        yield return wait;
    }
}
