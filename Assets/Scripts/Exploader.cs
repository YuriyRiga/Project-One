using System.Collections.Generic;
using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 10f;

    public void ApplyExplosionToCubes(List<CubeData> cubes)
    {
        foreach (CubeData cubeData in cubes)
        {
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
            cubeData.CubeRigidbody.AddForce(randomDirection * _explosionForce, ForceMode.Impulse);
        }
    }
}
