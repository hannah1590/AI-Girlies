using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleShipGame : MonoBehaviour
{
    GridManager gridManager;
    AIManager aiManager;
    PlayerTurn playerTurn;
    Player player;

    public static GridTile[] playerGrid;
    public GridTile[] aiGrid;
    public static Turn turn = Turn.SETUP;

    private void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        aiManager = FindFirstObjectByType<AIManager>();
        playerTurn = FindFirstObjectByType<PlayerTurn>();
        player = FindFirstObjectByType<Player>();

        playerGrid = gridManager.InitalizeGrid(Vector2.zero - new Vector2(7,15));
        aiGrid = gridManager.InitalizeGrid(new Vector2(-7, 10));

        aiManager.MakeBoard();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (turn == Turn.SETUP)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turn == Turn.PLAYER && Input.GetMouseButtonDown(0))
        {
            playerTurn.Fire();
        }
        else if(turn == Turn.SETUP && Input.GetMouseButtonDown(0))
        {

        }
    }

    public void Setup(string tileName)
    {
        List<Vector2> boat = new List<Vector2>();

        Debug.Log(tileName);
        boat = player.PlaceBoat(tileName);
        int count = 0;

        switch(boat.Count)
        {
            case 2:
                if(player.destroyer2.Count != 0)
                {
                    count++;
                }
                break;
            case 3:
                if (player.crusier3.Count != 0 && player.submarine3.Count != 0)
                {
                    count++;
                }
                break;
            case 4:
                if (player.battleship4.Count != 0)
                {
                    count++;
                }
                break;
            case 5:
                if (player.carrier5.Count != 0)
                {
                    count++;
                }
                break;
        }

        if (count == 0)
        {
            foreach (Vector2 v in boat)
            {
                if (v.y >= gridManager.numRows || v.y < 0 || v.x >= gridManager.numColumns || v.x < 0 || BattleShipGame.playerGrid[(int)v.y * gridManager.numRows + (int)v.x].getStatus() != Status.EMPTY)
                {
                    Debug.Log("Your boat sucks");
                    count++;
                }
            }
        }
        if (count == 0)
        {
            foreach (Vector2 v in boat)
            {
                BattleShipGame.playerGrid[(int)v.y * gridManager.numRows + (int)v.x].SetShip();
                switch (boat.Count)
                {
                    case 2:
                        player.destroyer2 = boat;
                        break;
                    case 3:
                        if (player.crusier3.Count == 0)
                        {
                            player.crusier3 = boat;
                        }
                        else
                        {
                            player.submarine3 = boat;
                        }
                        break;
                    case 4:
                        player.battleship4 = boat;
                        break;
                    case 5:
                        player.carrier5 = boat;
                        break;
                }
            }
        }
    }
}
    public enum Turn
    {
        SETUP,
        PLAYER,
        AI
    }
