using TMPro;
using UnityEngine;

public class PoolStatsView : MonoBehaviour
{
    [SerializeField] private EntitySpawner _spawner;
    [SerializeField] private TMP_Text _spawnedCountText;
    [SerializeField] private TMP_Text _createdCountText;
    [SerializeField] private TMP_Text _activeCountText;

    private EntityPool<Entity> _pool;

    private void Start()
    {
        _pool = _spawner.GetPool();

        _pool.ObjectSpawnCountChanged += UpdateSpawnCountUI;
        _pool.ObjectCreatedCountChanged += UpdateCreatedCountUI;
        _pool.ActiveObjectCountChanged += UpdateActiveCountUI;
    }

    private void OnDestroy()
    {
        _pool.ObjectSpawnCountChanged -= UpdateSpawnCountUI;
        _pool.ObjectCreatedCountChanged -= UpdateCreatedCountUI;
        _pool.ActiveObjectCountChanged -= UpdateActiveCountUI;
    }

    private void UpdateSpawnCountUI(int count)
    {
        _spawnedCountText.text = count.ToString();
    }

    private void UpdateCreatedCountUI(int count)
    {
        _createdCountText.text = count.ToString();
    }

    private void UpdateActiveCountUI(int count)
    {
        _activeCountText.text = count.ToString();
    }
}
