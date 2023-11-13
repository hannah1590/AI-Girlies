using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BattleShipGame gameManager;
    [SerializeField] private AIManager aiManager;
    [SerializeField] private UIManager uiManager;

    private int rows;
    private int columns;

    private int carrier5Shot = 0;
    private int battleship4Shot = 0;
    private int crusier3Shot = 0;
    private int submarine3Shot = 0;
    private int destroyer2Shot = 0;


    // Player checks on the selected tile whether there is a ship there or not
    public void Fire()
    {
        GridTile tile = GetSelectedTile();
        if(tile != null)
        {
            tile.Fire(tile);
            if(aiManager.carrier5.Contains(tile.gridCords))
            {
                carrier5Shot++;
                uiManager.UpdateText("carrier", 5 - carrier5Shot);
                if (carrier5Shot == 5)
                    Debug.Log("Carrier Down");
            }
            if(aiManager.battleship4.Contains(tile.gridCords))
            {
                battleship4Shot++;
                uiManager.UpdateText("battleship", 4 - battleship4Shot);
                if (battleship4Shot == 4)
                    Debug.Log("Battleship Down");
            }
            if(aiManager.crusier3.Contains(tile.gridCords))
            {
                crusier3Shot++;
                uiManager.UpdateText("crusier", 3 - crusier3Shot);
                if (crusier3Shot == 3)
                    Debug.Log("Crusier Down");
            }
            if(aiManager.submarine3.Contains(tile.gridCords))
            {
                submarine3Shot++;
                uiManager.UpdateText("submarine", 3 - submarine3Shot);
                if (submarine3Shot == 3)
                    Debug.Log("Submarine Down");
            }
            if(aiManager.destroyer2.Contains(tile.gridCords))
            {
                destroyer2Shot++;
                uiManager.UpdateText("destroyer", 2 - destroyer2Shot);
                if (destroyer2Shot == 2)
                    Debug.Log("Destoryer Down");
            }
        }  
    }

    // Gets tile currently being hovered over by mouse
    private GridTile GetSelectedTile()
    {
        rows = gridManager.numRows;
        columns = gridManager.numColumns;

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                if (gameManager.aiGrid[i * rows + j].isHover)
                {
                    return gameManager.aiGrid[i * rows + j];
                }
            }
        }

        return null;
    }
}
