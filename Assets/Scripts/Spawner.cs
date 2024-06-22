using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _startScaleMultiplier = 2;
    [SerializeField] private float _rayLength = 100f;
    [SerializeField] private int _minCountCube = 2;
    [SerializeField] private int _maxCountCube = 6;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0.1f);
    [SerializeField] private Rigidbody _prefabCube;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _hitLayer;

    private List<Rigidbody> _newCubes = new List<Rigidbody>();
    private int _minRangeRandomChance = 0;
    private int _maxRangeRandomChance = 11;
    private int _divisionChance = 10;
    private Exploader _exploader;

    private void Start()
    {
        Rigidbody startCube = Instantiate(_prefabCube, new Vector3(1f, 1f, 0f), Quaternion.identity);
        startCube.transform.localScale = Vector3.one * _startScaleMultiplier;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseRaycast();
        }
    }

    private void HandleMouseRaycast()
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
            _exploader?.ExplosionCube(originalPosition, originalScale, _newCubes);
            _newCubes.Clear();
        }
    }
    private void CreateNewCube(Vector3 position, Vector3 scale)
    {
        Rigidbody newCube;
        int numberOfCubes = Random.Range(_minCountCube, _maxCountCube);

        for (int i = 0; i < numberOfCubes; i++)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            newCube = Instantiate(_prefabCube, position + _offset * i, Quaternion.identity);
            newCube.transform.localScale = scale;
            newCube.GetComponent<Renderer>().material.color = randomColor;

            _newCubes.Add(newCube);
        }
    }
}
