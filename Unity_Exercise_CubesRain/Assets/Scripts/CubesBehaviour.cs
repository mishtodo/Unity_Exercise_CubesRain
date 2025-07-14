using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CubeSpawner))]
public class CubesBehaviour : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private ReturnToPool[] _returnToPools;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        foreach (var item in _returnToPools)
            item.Entered += ReturnCube;
    }

    private void OnDisable()
    {
        foreach (var item in _returnToPools)
            item.Entered -= ReturnCube;
    }

    public void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public void RestartCoroutine(Cube cube)
    {
        _coroutine = StartCoroutine(WaitBeforeDestroyCube(cube));
    }

    private void ReturnCube(Cube cube)
    {
        RestartCoroutine(cube);
    }

    private IEnumerator WaitBeforeDestroyCube(Cube cube)
    {
        float minDestroyDelay = 2;
        float maxDestroyDelay = 5;
        float RandomDelay = Random.Range(minDestroyDelay, maxDestroyDelay);
        var wait = new WaitForSecondsRealtime(RandomDelay);

        yield return wait;
        _cubeSpawner.ReleaseCube(cube);
    }
}
