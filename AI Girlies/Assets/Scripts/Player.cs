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
    public int shipIndex = 0;
    public string currentBoat;
    [SerializeField] private int dirIndex = 0;
    public bool isPlacing = false;
    private List<Vector2> hoverBoat;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        hoverBoat = new List<Vector2>();
    }
    public void Update()
    {
       
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /*
        if (Input.mouseScrollDelta.y != 0f)
        {
            if (shipIndex < 5 && Input.mouseScrollDelta.y > 0.0f)
                shipIndex++;
            if (shipIndex > 2 && Input.mouseScrollDelta.y < 0.0f)
                shipIndex--;
        }
        */
        if(Input.GetMouseButtonDown(1)) {
            if (dirIndex < 3)
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

        // gets the current row and col of the current tile being selected
        foreach(GridTile tile in BattleShipGame.playerGrid)
        {
            if(tile.gameObject.name == tileName)
            {
                currentRow = tile.gridCords.y;
                currentCol = tile.gridCords.x;
            }
        }

        // makes sure that the boat is placed with the selected tile in the middle
        int limit;
        if(shipIndex % 2 == 0)
        {
            limit = 0;
        }
        else
        {
            limit = 1;
        }

        // adds the surrounding tiles to boat according to the current direction
        for(int i = -shipIndex / 2; i < (shipIndex + limit) / 2; i++)
        {
            switch(dirIndex)
            {
                case 0:
                    boat.Add(new Vector2(currentCol, currentRow + i));
                    break;
                case 1:
                    boat.Add(new Vector2(currentCol + i, currentRow));
                    break;
                case 2:
                    boat.Add(new Vector2(currentCol, currentRow - i));
                    break;
                case 3:
                    boat.Add(new Vector2(currentCol - i, currentRow));
                    break;
            }
        }

        return boat;
    }

    // highlights the tiles the boat will be placed on during setup
    public void HighlightBoat(string tileName)
    {
        if(hoverBoat.Count != 0)
        {
            foreach (Vector2 tile in hoverBoat)
            {
                if (tile.y < gridManager.numRows && tile.y >= 0 && tile.x < gridManager.numColumns && tile.x >= 0)
                    BattleShipGame.playerGrid[(int)tile.y * gridManager.numRows + (int)tile.x].ShipHoverReset();
            }
        }

        if (isPlacing)
        {
            hoverBoat = PlaceBoat(tileName);
            foreach (Vector2 tile in hoverBoat)
            {
                if (tile.y < gridManager.numRows && tile.y >= 0 && tile.x < gridManager.numColumns && tile.x >= 0)
                    BattleShipGame.playerGrid[(int)tile.y * gridManager.numRows + (int)tile.x].ShipHoverSet();
            }
        }
    }
}