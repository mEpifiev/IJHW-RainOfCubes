using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const float MinDelay = 2f;
    private const float MaxDelay = 5f;

    [SerializeField] private ColorChanger _colorChanger;

    private bool _isTouched;

    public event Action<Cube> Released;

    private void OnEnable()
    {
        _isTouched = false;
    }

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() == null)
            return;

        if(_isTouched)
            return;

        _isTouched = true;
        _colorChanger.Change();

        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        float randomDelay = UnityEngine.Random.Range(MinDelay, MaxDelay + 1f);

        yield return new WaitForSeconds(randomDelay);

        Released?.Invoke(this);
    }
}
