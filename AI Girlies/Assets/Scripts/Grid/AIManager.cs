using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BattleShipGame battleShipGame;

    private int rows;
    private int columns;

    public List<Vector2> carrier5;
    public List<Vector2> battleship4;
    public List<Vector2> crusier3;
    public List<Vector2> submarine3;
    public List<Vector2> destroyer2;

    private Mode mode = Mode.HUNT;
    private int[] shipz = { 2, 3, 3, 4, 5 };
    [SerializeField] private int[] probabilityMap;

    private void Start()
    {
        //gridManager = FindFirstObjectByType<GridManager>();
        //aiGrid = FindFirstObjectByType<BattleShipGame>();
    }
    public void MakeBoard()
    {
        rows = gridManager.numRows;
        columns = gridManager.numColumns;

        probabilityMap = new int[rows * columns];
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
            currentRow = battleShipGame.aiGrid[loc].gridCords.x;
            currentCol = battleShipGame.aiGrid[loc].gridCords.y;

            if (battleShipGame.aiGrid[loc].getStatus() == Status.EMPTY)
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
                                    if(battleShipGame.aiGrid[(currentRow + i) * rows + currentCol].getStatus() == Status.EMPTY)
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
                                    if (battleShipGame.aiGrid[currentRow * rows + (currentCol + i)].getStatus() == Status.EMPTY)
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
                                    if (battleShipGame.aiGrid[(currentRow - i) * rows + currentCol].getStatus() == Status.EMPTY)
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
                                    if (battleShipGame.aiGrid[currentRow * rows + (currentCol - i)].getStatus() == Status.EMPTY)
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
                    battleShipGame.aiGrid[(currentRow + i) * rows + currentCol].SetShip();
                    break;
                case 1:
                    boat.Add(new Vector2(currentCol + i, currentRow));
                    battleShipGame.aiGrid[currentRow * rows + (currentCol + i)].SetShip();
                    break;
                case 2:
                    boat.Add(new Vector2(currentCol, currentRow - i));
                    battleShipGame.aiGrid[(currentRow - i) * rows + currentCol].SetShip();
                    break;
                case 3:
                    boat.Add(new Vector2(currentCol - i, currentRow));
                    battleShipGame.aiGrid[currentRow * rows + (currentCol - i)].SetShip();
                    break;
            }
        }

        return boat;
    }

    public void Shoot()
    {
        GridTile tile = new GridTile();

        if(mode == Mode.HUNT) { 
            huntMode();
        }
        else { targetMode(); }
    }

    public int huntMode()
    {
        GenerateProbMap();
        List<int> list = new List<int>();
        int biggest = 0;
        int final = 0;
        for (int i = 0; i < probabilityMap.Length; i++)
        {
            if (probabilityMap[i] > biggest)
            {
                list.Clear();
                biggest = probabilityMap[i];
                list.Add(i);
            }
            if (probabilityMap[i] == biggest)
            {
                list.Add(i);
            }
        }

        if (list.Count > 1)
            final = Random.RandomRange(0, list.Count);
        return final;
    }

    public void targetMode()
    {

    }

    public void GenerateProbMap()
    {
        for (int i = 0; i < rows * columns; i++)
        {
            probabilityMap[i] = 0;
        }

        for (int i = 0; i < shipz.Length; i++)
        {
            int currentShip = shipz[i];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    for (int dir = 0; dir < 4; dir++)
                    {
                        switch (dir)
                        {
                            case 0: // North
                                if (y + currentShip < gridManager.numRows)
                                {
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        probabilityMap[x * rows + (y + j)] += 1;
                                        BattleShipGame.playerGrid[x * rows + (y + j)].probability += 1;
                                    }
                                }
                                break;
                            case 1: // East
                                if (x + currentShip < gridManager.numColumns)
                                {
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        probabilityMap[(x + j) * rows + y] += 1;
                                        BattleShipGame.playerGrid[(x + j) * rows + y].probability += 1;
                                    }
                                }
                                break;
                            case 2: // South
                                if (y - currentShip >= 0)
                                {
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        probabilityMap[x * rows + (y - j)] += 1;
                                        BattleShipGame.playerGrid[x * rows + (y - j)].probability += 1;
                                    }
                                }
                                break;
                            case 3: // West
                                if (x - currentShip >= 0)
                                {
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        probabilityMap[(x - j) * rows + y] += 1;
                                        BattleShipGame.playerGrid[(x - j) * rows + y].probability += 1;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
public enum Mode
{
    HUNT,
    TARGET
}