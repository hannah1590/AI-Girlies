using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private GridManager gridManager;
    private BattleShipGame aiGrid;

    private int rows;
    private int columns;

    public List<Vector2> carrier5;
    public List<Vector2> battleship4;
    public List<Vector2> crusier3;
    public List<Vector2> submarine3;
    public List<Vector2> destroyer2;

    private void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        aiGrid = FindFirstObjectByType<BattleShipGame>();
    }
    public void MakeBoard()
    {
        rows = gridManager.numRows;
        columns = gridManager.numColumns;

        carrier5 = CreateBoat(5);
        battleship4 = CreateBoat(4);
        crusier3 = CreateBoat(3);
        submarine3 = CreateBoat(3);
        destroyer2 = CreateBoat(2);
    }

    private List<Vector2> CreateBoat(int size)
    {
        int loc;
        int dir = Random.Range(0, 4); // 0 = North, 1 = East, 2 = South, 3 = West
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
            currentRow = aiGrid.aiGrid[loc].gridCords.x;
            currentCol = aiGrid.aiGrid[loc].gridCords.y;

            if (aiGrid.aiGrid[loc].getStatus() == Status.EMPTY)
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
                            for(int i = 0; i < size; i++)
                            {
                                if(currentRow + i < rows)
                                {
                                    if(aiGrid.aiGrid[(currentRow + i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 1:
                            for (int i = 0; i < size; i++)
                            {
                                if (currentCol + i < columns)
                                {
                                    if (aiGrid.aiGrid[currentRow * rows + (currentCol + i)].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < size; i++)
                            {
                                if (currentRow - i >= 0)
                                {
                                    if (aiGrid.aiGrid[(currentRow - i) * rows + currentCol].getStatus() == Status.EMPTY)
                                    {
                                        count++;
                                    }
                                }
                            }
                            break;
                        case 3:
                            for (int i = 0; i < size; i++)
                            {
                                if (currentCol - i >= 0)
                                {
                                    if (aiGrid.aiGrid[currentRow * rows + (currentCol - i)].getStatus() == Status.EMPTY)
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


        for (int i = 0; i < size; i++)
        {
            switch (dir)
            {
                case 0:
                    boat.Add(new Vector2(currentCol, currentRow + i));
                    aiGrid.aiGrid[(currentRow + i) * rows + currentCol].SetShip();
                    break;
                case 1:
                    boat.Add(new Vector2(currentCol + i, currentRow));
                    aiGrid.aiGrid[currentRow * rows + (currentCol + i)].SetShip();
                    break;
                case 2:
                    boat.Add(new Vector2(currentCol, currentRow - i));
                    aiGrid.aiGrid[(currentRow - i) * rows + currentCol].SetShip();
                    break;
                case 3:
                    boat.Add(new Vector2(currentCol - i, currentRow));
                    aiGrid.aiGrid[currentRow * rows + (currentCol - i)].SetShip();
                    break;
            }
        }

        return boat;
    }
}
