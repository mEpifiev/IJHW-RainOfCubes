using System;
using UnityEngine;
using UnityEngine.Pool;

public class EntityPool<T> where T : Entity
{
    private T _prefab;
    private ObjectPool<T> _pool = null;
    private int _capacity = 5;
    private int _maxSize = 20;
    private int _spawnedCount = 0;
    private int _createdCount = 0;

    public EntityPool(T prefab)
    {
        _prefab = prefab;
    }

    public event Action<Entity> ObjectReleased;
    public event Action<int> ObjectSpawnCountChanged;
    public event Action<int> ObjectCreatedCountChanged;
    public event Action<int> ActiveObjectCountChanged;

    public void Initialize()
    {
        _pool ??= new ObjectPool<T>(
            createFunc: CreateObject,
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: (obj) => GameObject.Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _capacity,
            maxSize: _maxSize);
    }

    public T Get() =>
        _pool.Get();

    public void Release(T obj) => 
        _pool.Release(obj);

    private T CreateObject()
    {
        _createdCount++;

        ObjectCreatedCountChanged?.Invoke(_createdCount);

        return GameObject.Instantiate(_prefab);
    }

    private void OnGetObject(T obj)
    {
        _spawnedCount++;

        obj.gameObject.SetActive(true);
        obj.Initialize();

        ObjectSpawnCountChanged?.Invoke(_spawnedCount);
        ActiveObjectCountChanged?.Invoke(_pool.CountActive);

        obj.SubscribeToReleased(OnReleased);
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);

        ActiveObjectCountChanged?.Invoke(_pool.CountActive);
        ObjectReleased?.Invoke(obj);
        obj.UnsubscribeFromReleased(OnReleased);
    }

    private void OnReleased(Entity entity)
    {
        _pool.Release((T)entity);
    }
}