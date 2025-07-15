using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubesPool;
    private Coroutine _coroutine;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;
    private int _randomScale = 10;
    private float _spawnRepeatRate = 0.5f;

    private void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab, CalculateRandomPosition(), Quaternion.identity),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => ActionOnRelease(cube),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolDefaultCapacity,
        maxSize: _poolMaxCapacity);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _spawnRepeatRate);
    }

    private Vector3 CalculateRandomPosition()
    {
        Vector3 randomSpawnPosition;
        randomSpawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * _randomScale, 0.0f, Random.insideUnitCircle.y * _randomScale);
        return randomSpawnPosition;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Dying += RestartCoroutine;
        cube.InitializeHitted(false);
        cube.InitializePosition(CalculateRandomPosition());
        cube.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.Dying -= RestartCoroutine;
        cube.SetActive(false);
    }

    private void ReleaseCube(Cube cube)
    {
        _cubesPool.Release(cube);
    }

    private void GetCube()
    {
        _cubesPool.Get();
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

    private IEnumerator WaitBeforeDestroyCube(Cube cube)
    {
        float minDestroyDelay = 2;
        float maxDestroyDelay = 5;
        float RandomDelay = Random.Range(minDestroyDelay, maxDestroyDelay);
        var wait = new WaitForSecondsRealtime(RandomDelay);

        yield return wait;
        ReleaseCube(cube);
    }
}
