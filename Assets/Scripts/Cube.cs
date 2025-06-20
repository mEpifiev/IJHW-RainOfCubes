using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float MinDelay = 2f;
    private const float MaxDelay = 5f;

    [SerializeField] private Color _color;

    private bool _isTouched;

    private CubePool _cubePool;
    private Renderer _renderer;

    private void OnEnable()
    {
        _isTouched = false;
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(CubePool cubePool)
    {
        _cubePool = cubePool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() == null)
            return;

        if(_isTouched)
            return;

        _isTouched = true;
        _renderer.material.color = _color;

        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        float randomDelay = Random.Range(MinDelay, MaxDelay + 1f);

        yield return new WaitForSeconds(randomDelay);

        _cubePool.Release(this);
    }
}
