using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Cube : Entity
{
    private const float MinDelay = 2f;
    private const float MaxDelay = 5f;

    [SerializeField] private Color _colorAfterTouch;

    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;

    private bool _isTouched;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Platform>() == null)
            return;

        if(_isTouched)
            return;

        _isTouched = true;
        _colorChanger.Change(_colorAfterTouch);

        StartCoroutine(CountToDestroy());
    }

    public override void Initialize()
    {
        _isTouched = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator CountToDestroy()
    {
        float randomDelay = Random.Range(MinDelay, MaxDelay + 1f);

        yield return new WaitForSeconds(randomDelay);

        InvokeReleased();
    }
}
