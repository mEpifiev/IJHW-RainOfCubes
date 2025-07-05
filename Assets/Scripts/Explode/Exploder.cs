using System;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 350f;

    public event Action Exploded;

    public void Explode()
    {
        foreach (Rigidbody hit in GetExplodableObjects())
            hit.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);

        Exploded?.Invoke();
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
