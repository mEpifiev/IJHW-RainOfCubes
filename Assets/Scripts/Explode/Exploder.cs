using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExplodeView))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 350f;

    private ExplodeView _view;

    private void Awake()
    {
        _view = GetComponent<ExplodeView>();
    }

    public void Explode()
    {
        foreach (Rigidbody hit in GetExplodableObjects())
            hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

        _view.Play();
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);

        return rigidbodies;
    }
}
