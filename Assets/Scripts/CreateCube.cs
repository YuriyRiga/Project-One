using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{
    [SerializeField] private float _rayLength = 100f;
    [SerializeField] private float _explosionRadius = 10f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private int _minCountCube = 2;
    [SerializeField] private int _maxCountCube = 6;

    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0.1f);
    [SerializeField] private GameObject _prefabCube;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _hitLayer;

    private int _minRangeRandomChance = 0;
    private int _maxRangeRandomChance = 11;
    private int _divisionChance = 10;

    void Start()
    {
        GameObject startCube = Instantiate(_prefabCube, new Vector3(1f, 1f, 0f), Quaternion.identity);
        startCube.transform.localScale = new Vector3(2, 2, 2);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRay();
        }
    }

    private void SendRay()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, _rayLength, _hitLayer))
        {
            DivideCube(raycastHit.collider.gameObject);
        }
    }

    private void DivideCube(GameObject hitObject)
    {
        int numberDivided = 2;

        Vector3 originalPosition = hitObject.transform.position;
        Vector3 originalScale = hitObject.transform.localScale;
        Destroy(hitObject);

        Vector3 newScale = originalScale / numberDivided;

        if (Random.Range(_minRangeRandomChance, _maxRangeRandomChance) <= _divisionChance)
        {
            _divisionChance /= numberDivided;
            CreateNewCube(originalPosition, newScale);
            ExplosionCube(originalPosition);
        }
    }

    private void CreateNewCube(Vector3 position, Vector3 scale)
    {
        GameObject newCube;
        int numberOfCubes = Random.Range(_minCountCube, _maxCountCube);

        for (int i = 0; i < numberOfCubes; i++)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            newCube = Instantiate(_prefabCube, position + offset * i, Quaternion.identity);
            newCube.transform.localScale = scale;
            newCube.GetComponent<Renderer>().material.color = randomColor;
        }
    }

    private void ExplosionCube(Vector3 position)
    {
        position.y = 0;
        Rigidbody rigidbody;

        Collider[] colliders = Physics.OverlapSphere(position, _explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            rigidbody = nearbyObject.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius);
            }
        }
    }
}