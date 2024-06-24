using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _startScaleMultiplier = 2;
    [SerializeField] private float _rayLength = 100f;
    [SerializeField] private int _minCountCube = 2;
    [SerializeField] private int _maxCountCube = 6;
    [SerializeField] private Vector3 _offset = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Rigidbody _prefabCube;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _hitLayer;
    [SerializeField] private Exploader _exploader;

    private int _minRangeRandomChance = 0;
    private int _maxRangeRandomChance = 11;

    private void Start()
    {
        Rigidbody startCube = Instantiate(_prefabCube, new Vector3(1f, 1f, 0f), Quaternion.identity);
        startCube.transform.localScale = Vector3.one * _startScaleMultiplier;
        CubeDivisionChance cubeDivisionChance = startCube.GetComponent<CubeDivisionChance>();
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
        List<CubeData> newCubes = new List<CubeData>();

        Vector3 originalPosition = hitObject.transform.position;
        Vector3 originalScale = hitObject.transform.localScale;
        CubeDivisionChance parentsDivisionChance = hitObject.GetComponent<CubeDivisionChance>();
        Destroy(hitObject);

        Vector3 newScale = originalScale / numberDivided;

        if (Random.Range(_minRangeRandomChance, _maxRangeRandomChance) <= parentsDivisionChance.DivisionChance && parentsDivisionChance.DivisionChance > 0)
        {
            parentsDivisionChance.SetDivisionChance(parentsDivisionChance.DivisionChance / numberDivided);
            newCubes = CreateNewCubes(originalPosition, newScale, parentsDivisionChance);
            _exploader?.ApplyExplosionToCubes(newCubes);
        }
    }

    private List<CubeData> CreateNewCubes(Vector3 position, Vector3 scale, CubeDivisionChance divisionChance)
    {
        List<CubeData> newCubes = new List<CubeData>();
        int numberOfCubes = Random.Range(_minCountCube, _maxCountCube);

        float angleStep = 360f / numberOfCubes;
        float radius = scale.magnitude;

        for (int i = 0; i < numberOfCubes; i++)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);

            float angle = i * angleStep;
            Vector3 newPosition = position + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            Rigidbody newCube = Instantiate(_prefabCube, newPosition, Quaternion.identity);
            newCube.transform.localScale = scale;
            newCube.GetComponent<Renderer>().material.color = randomColor;

            CubeDivisionChance cubeDivisionChance = newCube.GetComponent<CubeDivisionChance>();
            cubeDivisionChance.SetDivisionChance(divisionChance.DivisionChance);
            CubeData cubeData = new CubeData(newCube, cubeDivisionChance);

            newCubes.Add(cubeData);
        }

        return newCubes;
    }
}
