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
    private GridTile[] shipTwo;
    private GridTile[] shipThree;
    private GridTile[] shipThreeTwo;
    private GridTile[] shipFour;    
    private GridTile[] shipFive;
    private int[] ships = { 2, 3, 3, 4, 5 };
    private int shipIndex = 0;
    private int dirIndex = 0;

  
    public void Update()
    {
       
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Input.mouseScrollDelta.y != 0f)
        {
            if (shipIndex < 5 && Input.mouseScrollDelta.y > 0.0f)
                shipIndex++;
            if (shipIndex > 0 && Input.mouseScrollDelta.y < 0.0f)
                shipIndex--;
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

        int loc;
        int dir = dirIndex; // 0 = North, 1 = East, 2 = South, 3 = West
        bool dirGood = false;
        bool locGood = false;
        int currentRow;
        int currentCol;
        int dirCount = 0;

        List<Vector2> boat = new List<Vector2>();

        // gets new loc if it is not available or all directions are blocked
        do
        {
            loc = Random.Range(0, rows * columns);
            currentRow = BattleShipGame.playerGrid[loc].gridCords.x;
            currentCol = BattleShipGame.playerGrid[loc].gridCords.y;

            if (BattleShipGame.playerGrid[loc].getStatus() == Status.EMPTY)
            {
                do
                {
                    int count = 0;

                    if (dirCount == 4) // checks if went through all directions
                    {
                        dirGood = true;
                        locGood = false;
                        dirCount = 0;
                    }
                    switch (dir)
                    {
                        case 0:
                            for (int i = 0; i < shipIndex; i++)
                            {
                                if (currentRow + i < rows)
                                {
                                    if (BattleShipGame.playerGrid[(currentRow + i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 1:
                            for (int i = 0; i < shipIndex; i++)
                            {
                                if (currentCol + i < columns)
                                {
                                    if (BattleShipGame.playerGrid[currentRow * rows + (currentCol + i)].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < shipIndex; i++)
                            {
                                if (currentRow - i >= 0)
                                {
                                    if (BattleShipGame.playerGrid[(currentRow - i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 3:
                            for (int i = 0; i < shipIndex; i++)
                            {
                                if (currentCol - i >= 0)
                                {
                                    if (BattleShipGame.playerGrid[currentRow * rows + (currentCol - i)].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                    }
                    if (count == shipIndex)
                    {
                        dirGood = true;
                        locGood = true;
                    }
                    else
                    {
                        // cycles through directions if current direction is not valid
                        dir++;
                        dirCount++;
                        if (dir > 3)
                            dir = 0;
                    }
                } while (!dirGood);
            }
        } while (!locGood);


 /*       for (int i = 0; i < size; i++)
        {
            boat.Add(new Vector2(currentRow + i, currentCol + i));
            switch (dir)
            {
                case 0:
                    BattleShipGame.playerGrid[(currentRow + i) * rows + currentCol].SetShip();
                    break;
                case 1:
                    BattleShipGame.playerGridme.playerGrid[currentRow * rows + (currentCol + i)].SetShip();
                    break;
                case 2:
                    BattleShipGame.playerGridme.playerGrid[(currentRow - i) * rows + currentCol].SetShip();
                    break;
                case 3:
                    BattleShipGame.playerGridme.playerGrid[currentRow * rows + (currentCol - i)].SetShip();
                    break;
            }
        }*/

        return boat;
    }

}