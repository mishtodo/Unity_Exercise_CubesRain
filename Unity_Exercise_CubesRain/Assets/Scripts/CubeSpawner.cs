using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _spawnRepeatRate = 0.5f;

    private ObjectPool<Cube> _cubesPool;
    private Coroutine _coroutine;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;
    private int _randomScale = 10;

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
        var wait = new WaitForSecondsRealtime(_spawnRepeatRate);

        while (true)
        {
            yield return wait;
            GetCube();
        }
    }

    private Vector3 CalculateRandomPosition()
    {
        Vector3 randomSpawnPosition;
        randomSpawnPosition = transform.position + new Vector3(Random.insideUnitCircle.x * _randomScale, 0.0f, Random.insideUnitCircle.y * _randomScale);
        return randomSpawnPosition;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.Dying += ReleaseCube;
        cube.InitializeHitted(false);
        cube.InitializePosition(CalculateRandomPosition());
        cube.SetActive(true);
    }

    private void ActionOnRelease(Cube cube)
    {
        cube.Dying -= ReleaseCube;
        cube.InitializePosition(transform.position);
        cube.InitializeRotation(transform.rotation);
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
}
