using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Ray ray;
    RaycastHit2D rayHit;
   [SerializeField] private GridManager gridManager;
    public List<Vector2> carrier5;
    public List<Vector2> battleship4;
    public List<Vector2> crusier3;
    public List<Vector2> submarine3;
    public List<Vector2> destroyer2;
    private int[] ships = { 2, 3, 3, 4, 5 };
    private int shipIndex = 4;
    private int dirIndex = 0;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }
    public void Update()
    {
       
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.mouseScrollDelta.y != 0f)
        {
            if (shipIndex < 5 && Input.mouseScrollDelta.y > 0.0f)
                shipIndex++;
            if (shipIndex > 2 && Input.mouseScrollDelta.y < 0.0f)
                shipIndex--;
            Debug.Log(shipIndex);
        }
        if(Input.GetMouseButtonDown(1)) {
            if (dirIndex < 5)
                dirIndex++;
            else dirIndex = 0;
        }
    }

    public List<Vector2> PlaceBoat(string tileName)
    {
        
        int rows = gridManager.numRows;
        int columns = gridManager.numColumns;

        int currentRow = 0;
        int currentCol = 0;

        List<Vector2> boat = new List<Vector2>();

        foreach(GridTile tile in BattleShipGame.playerGrid)
        {
            if(tile.gameObject.name == tileName)
            {
                currentRow = tile.gridCords.y;
                currentCol = tile.gridCords.x;
                Debug.Log(tile.gridCords);
            }
        }

        int limit;
        if(shipIndex % 2 == 0)
        {
            limit = 0;
        }
        else
        {
            limit = 1;
        }
        for(int i = -shipIndex / 2; i < (shipIndex + limit) / 2; i++)
        {
            Debug.Log(i);
            switch(dirIndex)
            {
                case 0:
                    boat.Add(new Vector2(currentCol, currentRow + i));
                    //BattleShipGame.playerGrid[(currentRow + i) * rows + currentCol].SetShip();
                    break;
                case 1:
                    boat.Add(new Vector2(currentCol + i, currentRow));
                    //BattleShipGame.playerGrid[currentRow * rows + (currentCol + i)].SetShip();
                    break;
                case 2:
                    boat.Add(new Vector2(currentCol, currentRow - i));
                    //BattleShipGame.playerGrid[(currentRow - i) * rows + currentCol].SetShip();
                    break;
                case 3:
                    boat.Add(new Vector2(currentCol - i, currentRow));
                    //BattleShipGame.playerGrid[currentRow * rows + (currentCol - i)].SetShip();
                    break;
            }
        }

        return boat;
    }
}