using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private int rows;
    private int columns;

    public void MakeBoard()
    {
        rows = gridManager.numRows;
        columns = gridManager.numColumns;

        CreateBoat(5);
        CreateBoat(4);
        CreateBoat(3);
        CreateBoat(3);
        CreateBoat(2);
    }

    private void CreateBoat(int size)
    {
        /*
        int loc;
        int dir = Random.Range(0, 4); // 0 = North, 1 = East, 2 = South, 3 = West
        bool dirGood = false;
        bool locGood = false;
        int currentRow;
        int currentCol;
        int dirCount = 0;

        // gets new loc if it is not available or all directions are blocked
        do
        {
            loc = Random.Range(0, rows * columns); Debug.Log(loc);
            currentRow = gridManager.tiles[loc].gridCords.x;
            currentCol = gridManager.tiles[loc].gridCords.y;

           

            if (gridManager.tiles[loc].getStatus() == Status.EMPTY)
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
                            for(int i = 1; i <= size; i++)
                            {
                                if(currentRow + i < rows)
                                {
                                    if(gridManager.tiles[(currentRow + i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 1:
                            for (int i = 1; i <= size; i++)
                            {
                                if (currentCol + i < columns)
                                {
                                    if (gridManager.tiles[currentRow * rows + (currentCol + i)].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (int i = 1; i <= size; i++)
                            {
                                if (currentRow - i >= 0)
                                {
                                    if (gridManager.tiles[(currentRow - i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 3:
                            for (int i = 1; i <= size; i++)
                            {
                                if (currentCol - i >= 0)
                                {
                                    if (gridManager.tiles[currentRow * rows + (currentCol - i)].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                    }
                    if (count == size)
                    {
                        dirGood = true;
                        locGood = true;
                    }
                    // cycles through directions if current direction is not valid
                    dir++;
                    if (dir > 3)
                        dir = 0;

                } while (!dirGood);
            }
        } while (!locGood);


        for (int i = 1; i <= size; i++)
        {
            switch (dir)
            {
                case 0:
                    gridManager.tiles[(currentRow + i) * rows + currentCol].SetShip();
                    break;
                case 1:
                    gridManager.tiles[currentRow * rows + (currentCol + i)].SetShip();
                    break;
                case 2:
                    gridManager.tiles[(currentRow - i) * rows + currentCol].SetShip();
                    break;
                case 3:
                    gridManager.tiles[currentRow * rows + (currentCol - i)].SetShip();
                    break;
            }
        }
        */
    }
}
