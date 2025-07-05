using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
[RequireComponent(typeof(Exploder))]
public class Bomb : Entity
{
    private const float MinDuration = 2f;
    private const float MaxDuration = 5f;

    [SerializeField] private Color _color;

    private Rigidbody _rigidbody;
    private Exploder _exploder;
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _exploder = GetComponent<Exploder>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    public override void Initialize(Entity entity)
    {
        Rigidbody entityRigidbody = entity.GetComponent<Rigidbody>();

        _rigidbody.velocity = entityRigidbody.velocity;
        _rigidbody.angularVelocity = entityRigidbody.angularVelocity;
        transform.position = entity.transform.position;
        transform.rotation = entity.transform.rotation;

        _colorChanger.Change(_color);
    }

    public void Explode()
    {
        float randomTimeToExplode = Random.Range(MinDuration, MaxDuration + 1);

        StartCoroutine(CountdownToExplode(randomTimeToExplode));
    }

    private IEnumerator CountdownToExplode(float time)
    {
        float elapsedTime = 0f;

        while(elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            Color newAlpha = _colorChanger.GetIntermediateAlpha(elapsedTime, time);
            _colorChanger.Change(newAlpha);

            yield return null;
        }

        _exploder.Explode();
        InvokeReleased();
    }
}
