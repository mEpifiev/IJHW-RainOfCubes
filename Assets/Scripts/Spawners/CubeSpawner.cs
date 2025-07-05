using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : EntitySpawner
{
    [SerializeField] private List<Vector3> _postions;
    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds delay = new WaitForSeconds(_delay);

        while (enabled)
        {
            yield return delay;

            Vector3 randomPosition = _postions[Random.Range(0, _postions.Count)];

            Cube newCube = (Cube)Pool.Get();
            
            newCube.transform.position = randomPosition;
        }
    }
}
