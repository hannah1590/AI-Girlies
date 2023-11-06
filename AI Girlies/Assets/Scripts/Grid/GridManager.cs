using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int numRows = 10;
    public int numColumns = 10;

    public float padding = 2f;

    [SerializeField] private GridTile tilePrefab;
    public GridTile[] tiles;

    public GridTile[] InitalizeGrid(Vector2 pos)
    {
        int p = 0;

        tiles = new GridTile[numRows * numColumns];
        for (int i = 0; i < numRows; i++)
        {
            p = 0;
            for(int j = 0; j < numColumns; j++)
            {
                GridTile tile = Instantiate(tilePrefab, transform);
                Vector2 tilePos;
                tilePos = new Vector2(i + (padding * i), j + (padding * j));
                tilePos += pos;
          

                tile.transform.localPosition = tilePos;
                tile.name = $"Tile_{p + (j % 2)}_{j}";
                tile.gridCords = new Vector2Int(p + (j % 2), j);
                tiles[j * numColumns + i] = tile;
            }
        }
        return tiles;
    }
    

}
