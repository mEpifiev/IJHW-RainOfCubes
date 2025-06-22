using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePool;
    [SerializeField] private List<Vector3> _postions;
    [SerializeField] private float _delay = 1f;

    private bool _isSpawning = true;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (_isSpawning)
        {
            yield return delay;

            Vector3 randomPosition = _postions[Random.Range(0, _postions.Count)];

            Cube newCube = _cubePool.Get();
            newCube.transform.position = randomPosition;
        }
    }
}
