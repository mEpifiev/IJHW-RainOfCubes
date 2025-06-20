using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private int _capacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: Create,
            actionOnGet: (cube) => cube.gameObject.SetActive(true),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _capacity,
            maxSize: _maxSize);
    }

    public Cube Get() => _pool.Get();

    public void Release(Cube cube) => _pool.Release(cube);

    private Cube Create()
    {
        Cube newCube = Instantiate(_prefab);
        newCube.Initialize(this);

        return newCube;
    }
}
