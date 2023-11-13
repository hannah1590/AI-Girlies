using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numRows = 10;
    public int numColumns = 10;

    public float padding = 0.2f;

    [SerializeField] private GridTile tilePrefab;
    public GridTile[] tiles;
    public GridTile[] InitalizeGrid(Vector2 pos)
    {
        tiles = new GridTile[numRows * numColumns];
        for (int i = 0; i < numRows; i++)
        {
            for(int j = 0; j < numColumns; j++)
            {
                GridTile tile = Instantiate(tilePrefab, transform);
                Vector2 tilePos;
                tilePos = new Vector2(i + (padding * i), j + (padding * j));
                tilePos += pos;
          

                tile.transform.localPosition = tilePos;
                tile.name = $"Tile_{i}_{j}";
                tile.gridCords = new Vector2Int(i, j);
                tiles[j * numColumns + i] = tile;
            }
        }
        return tiles;
    }
}
