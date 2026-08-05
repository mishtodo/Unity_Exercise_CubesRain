using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnRepeatRate = 0.5f;

    private Coroutine _coroutine;
    private int _randomScale = 10;

    private void Start()
    {
        RestartCoroutine();
    }

    public void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void RestartCoroutine()
    {
        _coroutine = StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        CalculateRandomPosition();

        var wait = new WaitForSecondsRealtime(_spawnRepeatRate);

        while (enabled)
        {
            yield return wait;
            Spawn(CalculateRandomPosition());
        }
    }

    private Vector3 CalculateRandomPosition()
    {
        Vector3 spawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * _randomScale, 0.0f, Random.insideUnitCircle.y * _randomScale);
        return spawnPosition;
    }
}
