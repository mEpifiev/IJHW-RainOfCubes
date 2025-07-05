using UnityEngine;

public abstract class EntitySpawner : MonoBehaviour
{
    [SerializeField] private Entity _prefab;

    protected EntityPool<Entity> Pool;

    private void Awake()
    {
        Pool = new EntityPool<Entity>(_prefab);
        Pool.Initialize();
    }

    public EntityPool<Entity> GetPool()
    {
        return Pool;
    }
}
