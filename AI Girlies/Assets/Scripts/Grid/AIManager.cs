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
    [SerializeField] private Player player;

    private int rows;
    private int columns;

    public List<Vector2> carrier5;
    public List<Vector2> battleship4;
    public List<Vector2> crusier3;
    public List<Vector2> submarine3;
    public List<Vector2> destroyer2;

    private int carrier5Shot = 0;
    private int battleship4Shot = 0;
    private int crusier3Shot = 0;
    private int submarine3Shot = 0;
    private int destroyer2Shot = 0;

    private Mode mode = Mode.HUNT;
    private int[] shipz = { 2, 3, 3, 4, 5 };
    [SerializeField] private int[] probabilityMap;
    private Stack<int> targetStack;

    private void Start()
    {
        //gridManager = FindFirstObjectByType<GridManager>();
        //aiGrid = FindFirstObjectByType<BattleShipGame>();
    }
    public void MakeBoard()
    {
        targetStack = new Stack<int>();
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
        bool hit = false;
        GridTile tile = new GridTile();
        int tileNum = 0;

        if(mode == Mode.HUNT) { 
            tileNum = huntMode();
        }
        else { 
            tileNum = targetMode(tileNum, hit); 
        }

        tile = BattleShipGame.playerGrid[tileNum];
        hit = tile.Fire(tile);
        if (hit)
        {
            mode = Mode.TARGET;
            changeMap(tileNum, -3);
            BattleShipGame.playerGrid[tileNum].probability = -3;
            if (player.carrier5.Contains(tile.gridCords))
            {
                carrier5Shot++;
                if(carrier5Shot == 5)
                {
                    shipz[4] = 0;
                }
            }
            if (player.battleship4.Contains(tile.gridCords))
            {
                battleship4Shot++;
                if (battleship4Shot == 4)
                {
                    shipz[3] = 0;
                }
            }
            if (player.crusier3.Contains(tile.gridCords))
            {
                crusier3Shot++;
                if (crusier3Shot == 3)
                {
                    shipz[2] = 0;
                }
            }
            if (player.submarine3.Contains(tile.gridCords))
            {
                submarine3Shot++;
                if (submarine3Shot == 3)
                {
                    shipz[1] = 0;
                }
            }
            if (player.destroyer2.Contains(tile.gridCords))
            {
                destroyer2Shot++;
                if (destroyer2Shot == 2)
                {
                    shipz[0] = 0;
                }
            }

            if(carrier5Shot == 5 || carrier5Shot == 0)
            {
                if (battleship4Shot == 4 || battleship4Shot == 0)
                {
                    if (crusier3Shot == 3 || crusier3Shot == 0)
                    {
                        if (submarine3Shot == 3 || submarine3Shot == 0)
                        {
                            if (destroyer2Shot == 2 || destroyer2Shot == 0)
                            {
                                mode = Mode.HUNT;
                            }
                        }
                    }
                }
            }
        }       
    }

    public int huntMode()
    {
        GenerateProbMap();
        List<int> list = new List<int>();
        int biggest = -1;
        int final = 0;
        for (int i = 0; i < probabilityMap.Length; i++)
        {
            if (probabilityMap[i] > biggest)
            {
                list.Clear();
                biggest = probabilityMap[i];
                list.Add(i);
            }
            else if (probabilityMap[i] == biggest)
            {
                list.Add(i);
            }
        }

        if (list.Count > 1)
        {
            int range = Random.RandomRange(0, list.Count);
            final = list[range];
        }
        else
        {
            
            final = list[0];

        }
        changeMap(final, -1);
        BattleShipGame.playerGrid[final].probability = -1;
        return final;
    }

    public int targetMode(int tile, bool hit)
    {
        if(hit)
            GenerateTargetMap(tile);
        if(targetStack.Count > 0)
        {
            int temp = targetStack.Pop();
            return temp;
        }
        return 0;
    }

    private bool alreadyShot(int place)
    {
        if (probabilityMap[place] == -1 || probabilityMap[place] == -3)
            return true;
        return false;
    }

    private void changeMap(int place, int num) //Put -2 into num if you just want to add one to the prexisting value
    {
        if (num == -2)
            probabilityMap[place] += 1;
        else
            probabilityMap[place] = num;
    }

    public void GenerateTargetMap(int tile)
    {
        int x = BattleShipGame.playerGrid[tile].gridCords.x;
        int y = BattleShipGame.playerGrid[tile].gridCords.y;

        for (int i = 0; i < rows * columns; i++)
        {
            if (!alreadyShot(i))
            {
                changeMap(i, 0);
                BattleShipGame.playerGrid[i].probability = 0;
            }
        }
        for (int dir = 0; dir < 4; dir++)
        {
            switch (dir)
            {
                case 0: // North
                    if (!alreadyShot(x * rows + (y + 1)))
                    {
                        targetStack.Push(x * rows + (y + 1));
                    }
                    break;
                case 1: // East
                    if (!alreadyShot((x + 1) * rows + y))
                    {
                        targetStack.Push((x + 1) * rows + y);
                    }
                    break;
                case 2: // South
                    if (!alreadyShot(x * rows + (y - 1)))
                    {
                        targetStack.Push(x * rows + (y - 1));
                    }
                    break;
                case 3: // West
                    if (!alreadyShot((x - 1) * rows + y))
                    {
                        targetStack.Push((x - 1) * rows + y);
                    }
                    break;
            }
        }
    }

    public void GenerateProbMap()
    {
        for (int i = 0; i < rows * columns; i++)
        {
            if (!alreadyShot(i))
            {
                changeMap(i, 0);
                BattleShipGame.playerGrid[i].probability = 0;
            }
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
                                    bool isShot = false;
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        if (alreadyShot(x * rows + (y + j)))
                                        {
                                            isShot = true;
                                            break;
                                        }
                                    }
                                    if(!isShot)
                                    {
                                        for (int j = 0; j < currentShip; j++)
                                        {
                                            changeMap(x * rows + (y + j), -2);
                                            BattleShipGame.playerGrid[x * rows + (y + j)].probability += 1;
                                        }
                                        isShot = false;
                                    }   
                                }
                                break;
                            case 1: // East
                                if (x + currentShip < gridManager.numColumns)
                                {
                                    bool isShot = false;
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        if (alreadyShot((x + j) * rows + y))
                                        {
                                            isShot = true;
                                            break;
                                        }
                                    }
                                    if (!isShot)
                                    {
                                        for (int j = 0; j < currentShip; j++)
                                        {
                                            changeMap((x + j) * rows + y, -2);
                                            BattleShipGame.playerGrid[(x + j) * rows + y].probability += 1;
                                        }
                                        isShot = false;
                                    }
                                }
                                break;
                            case 2: // South
                                if (y - currentShip >= 0)
                                {
                                    bool isShot = false;
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        if (alreadyShot(x * rows + (y - j)))
                                        {
                                            isShot = true;
                                            break;
                                        }
                                    }
                                    if (!isShot)
                                    {
                                        for (int j = 0; j < currentShip; j++)
                                        {
                                            changeMap(x * rows + (y - j), -2);
                                            BattleShipGame.playerGrid[x * rows + (y - j)].probability += 1;
                                        }
                                        isShot = false;
                                    }
                                }
                                break;
                            case 3: // West
                                if (x - currentShip >= 0)
                                {
                                    bool isShot = false;
                                    for (int j = 0; j < currentShip; j++)
                                    {
                                        if (alreadyShot((x - j) * rows + y))
                                        {
                                            isShot = true;
                                            break;
                                        }
                                    }
                                    if (!isShot)
                                    {
                                        for (int j = 0; j < currentShip; j++)
                                        {
                                            changeMap((x - j) * rows + y, -2);
                                            BattleShipGame.playerGrid[(x - j) * rows + y].probability += 1;
                                        }
                                        isShot = false;
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