using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubesPool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;
    private int _randomScale = 10;
    private float _spawnRepeatRate = 1.0f;

    private void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab, transform.position, Quaternion.identity),
        actionOnGet: (cube) => ActionOnGet(cube),
        actionOnRelease: (cube) => cube.SetActive(false),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: _poolDefaultCapacity,
        maxSize: _poolMaxCapacity);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _spawnRepeatRate);
    }

    public void ActionOnGet(Cube cube)
    {
        cube.InitializeHitted(false);
        cube.InitializePosition(transform.position);
        cube.SetActive(true);
    }

    public void ReleaseCube(Cube cube)
    {
        _cubesPool.Release(cube);
    }

    private void GetCube()
    {
        _cubesPool.Get();
    }
}
