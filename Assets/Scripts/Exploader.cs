using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _damageMultiplierForParents = 2f;

    public void ApplyExplosionToCubes(List<CubeData> cubes)
    {
        foreach (CubeData cubeData in cubes)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            cubeData.CubeRigidbody.AddForce(randomDirection * _explosionForce, ForceMode.Impulse);
        }
    }
    
    public void ApplyExplosionParentsCube(Vector3 position, Vector3 scale)
    {
        position.y = 0;
        Rigidbody rigidbody;

        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                float distance = Vector3.Distance(position, nearbyObject.transform.position);

                float normalizedDistance = Mathf.Clamp01(1 - (distance / _explosionRadius));

                float explosionForce = _explosionForce * _damageMultiplierForParents / scale.y * normalizedDistance;

                rigidbody.AddExplosionForce(explosionForce, position, _explosionRadius / scale.y);
            }
        }
    }
}
