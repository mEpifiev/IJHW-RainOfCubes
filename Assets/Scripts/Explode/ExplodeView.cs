using UnityEngine;

public class ExplodeView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    public void Play()
    {
        if (_effect == null)
            return;

        ParticleSystem effect = Instantiate(_effect, transform.position, Quaternion.identity);

        effect.Play();

        Destroy(effect.gameObject, effect.main.duration + effect.main.startLifetime.constantMax);
    }
}
