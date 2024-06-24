using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;

    public void ApplyExplosionToCubes(List<Rigidbody> cubes)
    {
        foreach (Rigidbody cubeRigidbody in cubes)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            cubeRigidbody.AddForce(randomDirection * _explosionForce, ForceMode.Impulse);
        }
    }
}
