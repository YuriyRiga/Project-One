using UnityEngine;

public class CubeDivisionChance : MonoBehaviour
{
    [SerializeField] private float _divisionChance = 10f;

    public float DivisionChance
    {
        get { return _divisionChance; }
        set { _divisionChance = value; }
    }
}
