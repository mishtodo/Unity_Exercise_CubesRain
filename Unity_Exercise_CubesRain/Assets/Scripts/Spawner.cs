using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : SpawnableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private GameObject _objectPool;

    private ObjectPool<T> _pool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxCapacity = 20;

    public event Action<Vector3> ObjectReleased;
    public int TotalSpawned { get; private set; }
    public int ActiveCount => _pool.CountActive;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab, Vector3.zero, Quaternion.identity),
            actionOnGet: (T) => ActionOnGet(T),
            actionOnRelease: (T) => ActionOnRelease(T),
            actionOnDestroy: (T) => Destroy(T),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxCapacity
        );
    }

    public T Spawn(Vector3 spawnPosition)
    {
        T obj = _pool.Get();
        obj.transform.position = spawnPosition;
        return obj;
    }

    private void ActionOnGet(T obj)
    {
        TotalSpawned++;
        obj.OnDying += HandleObjectDestroyed;
        obj.gameObject.SetActive(true);
    }

    private void ActionOnRelease(T obj)
    {
        obj.OnDying -= HandleObjectDestroyed;
        obj.InitializePosition(_objectPool.transform.position);
        obj.InitializeRotation(_objectPool.transform.rotation);
        obj.gameObject.SetActive(false);
    }

    private void HandleObjectDestroyed(SpawnableObject obj)
    {
        ObjectReleased?.Invoke(obj.transform.position);
        _pool.Release((T)obj);
    }
}
