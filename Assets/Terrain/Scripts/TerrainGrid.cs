using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class TerrainTile
{
    //define all of the values for the class
    public GameObject terrainPrefab;
    public float rotation;
    public bool isWalkable;

    //define a constructor for the class
    public TerrainTile()
    {

    }
}

public class TerrainGrid : SerializedMonoBehaviour
{
    // Start is called before the first frame update

    public TerrainTile[,] terrainConstruct;

}
