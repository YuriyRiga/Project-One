using UnityEngine;

public class CubeData
{
    public CubeData(Rigidbody rigidbody, CubeDivisionChance divisionChance)
    {
        CubeRigidbody = rigidbody;
        DivisionChance = divisionChance;
    }

    public Rigidbody CubeRigidbody { get; private set; }
    public CubeDivisionChance DivisionChance { get; private set; }
}