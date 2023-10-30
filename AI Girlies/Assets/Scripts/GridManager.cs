using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numRows = 10;
    public int numColumns = 10;

    public float padding = 0.0001f;

    [SerializeField] private GridTile tilePrefab;
    public GridTile[] tiles;


    public void InitializeGrid()
    {
        tiles = new GridTile[numRows * numColumns];
        for(int i = 0; i < numRows; i++)
        {
            for(int j = 0; j < numColumns; j++)
            {
                tiles += Instantiate(tilePrefab, transform);
            }
        }
    }
}
