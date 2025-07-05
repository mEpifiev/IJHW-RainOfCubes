using UnityEngine;

public class BombSpawner : EntitySpawner
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private EntityPool<Entity> _cubePool;

    private void Start()
    {
        _cubePool = _cubeSpawner.GetPool();

        _cubePool.ObjectReleased += Spawn;
    }

    private void OnDestroy()
    {
        _cubePool.ObjectReleased -= Spawn;
    }

    private void Spawn(Entity entity)
    {
        Bomb bomb = (Bomb)Pool.Get();

        bomb.Initialize(entity);
        bomb.Explode();
    }
}
