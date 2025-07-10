using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private ObjectPool<Cube> _cubesPool;
    private int _poolDefaultCapacity = 5;
    private int _poolMaxCapacity = 5;

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
        InvokeRepeating(nameof(GetCube), 0.0f, 0.3f);
    }

    public void ActionOnGet(Cube cube)
    {
        cube.InitializeHitted(false);
        cube.InitializePosition(transform.position);
        cube.SetActive(true);
    }

   public void ActionOnRelease(Cube cube)
   {
        _cubesPool.Release(cube);
   }

    private void GetCube()
    {
        _cubesPool.Get();
    }
}
