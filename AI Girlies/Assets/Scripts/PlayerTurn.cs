using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BattleShipGame aiGrid;

    private int rows;
    private int columns;

    public void Fire()
    {
        GridTile tile = GetSelectedTile();
        if(tile != null)
        {
            tile.Fire(tile);
        }  
    }

    private GridTile GetSelectedTile()
    {
        rows = gridManager.numRows;
        columns = gridManager.numColumns;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if (aiGrid.aiGrid[i * rows + j].isHover)
                {
                    return aiGrid.aiGrid[i * rows + j];
                }
            }
        }

        return null;
    }
}
