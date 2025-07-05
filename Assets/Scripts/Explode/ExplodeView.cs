using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class ExplodeView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private Exploder _exploder;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
    }

    private void OnEnable()
    {
        _exploder.Exploded += OnExploded;
    }

    private void OnDisable()
    {
        _exploder.Exploded -= OnExploded;
    }

    public void OnExploded()
    {
        if (_effect == null)
            return;

        ParticleSystem effect = Instantiate(_effect, transform.position, Quaternion.identity);

        effect.Play();

        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
    }
}
