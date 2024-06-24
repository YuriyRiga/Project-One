using UnityEngine;

public class CubeDivisionChance : MonoBehaviour
{
    [SerializeField] private int _divisionChance = 10;

    public int DivisionChance
    {
        get { return _divisionChance; }
        private set { _divisionChance = value; }
    }

    public void SetDivisionChance(int divisionChance)
    {
        _divisionChance = divisionChance;
    }
}
