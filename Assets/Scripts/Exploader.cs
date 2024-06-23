using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 10f;

    public void ApplyExplosionToCubes(Vector3 position, Vector3 scale, List<Rigidbody> cubes)
    {
        foreach (Rigidbody cubeRigidbody in cubes)
        {
            if (Vector3.Distance(position, cubeRigidbody.transform.position) <= _explosionRadius)
            {
                cubeRigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }

        }
    }
}
