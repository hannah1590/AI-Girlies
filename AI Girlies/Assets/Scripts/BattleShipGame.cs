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

    // Update is called once per frame
    void Update()
    {
        if(turn == Turn.PLAYER && Input.GetMouseButtonDown(0))
        {
            playerTurn.Fire();
            turn = Turn.AI;
        }
        else if(turn == Turn.AI)
        {
            //ai shoot
            
            aiManager.GenerateProbMap();
            aiManager.Shoot();
            turn = Turn.PLAYER;
        }
    }

    public void Setup(string tileName)
    {
        List<Vector2> boat = new List<Vector2>();

        //Debug.Log(tileName);
        boat = player.PlaceBoat(tileName);
        int count = 0;

        // checks if boat has already been placed
        switch(boat.Count)
        {
            case 2:
                if(player.destroyer2.Count != 0 && player.currentBoat == "destroyer")
                {
                    count++;
                }
                break;
            case 3:
                if (player.crusier3.Count != 0 && player.currentBoat == "crusier")
                {
                    count++;
                }
                if(player.submarine3.Count != 0 && player.currentBoat == "submarine")
                {
                    count++;
                }
                break;
            case 4:
                if (player.battleship4.Count != 0 && player.currentBoat == "battleship")
                {
                    count++;
                }
                break;
            case 5:
                if (player.carrier5.Count != 0 && player.currentBoat == "carrier")
                {
                    count++;
                }
                break;
        }

        if (count == 0)
        {
            // checks if boat is in bounds
            foreach (Vector2 v in boat)
            {
                if (v.y >= gridManager.numRows || v.y < 0 || v.x >= gridManager.numColumns || v.x < 0 || BattleShipGame.playerGrid[(int)v.y * gridManager.numRows + (int)v.x].getStatus() != Status.EMPTY)
                {
                    Debug.Log("Your boat sucks");
                    count++;
                }
            }
        }

        // if boat has not already been placed and is in bounds then add it to the correct player boat
        if (count == 0)
        {
            if(player.currentBoat == "carrier")
            {
                player.carrier5 = boat;
            }
            else if(player.currentBoat == "battleship")
            {
                player.battleship4 = boat;
            }
            else if(player.currentBoat == "crusier")
            {
                player.crusier3 = boat;
            }
            else if(player.currentBoat == "submarine")
            {
                player.submarine3 = boat;
            }
            else if(player.currentBoat == "destroyer")
            {
                player.destroyer2 = boat;
            }
            foreach (Vector2 v in boat)
            {
                playerGrid[(int)v.y * gridManager.numRows + (int)v.x].SetShip();
            } 
            player.isPlacing = false;
        }

        int amountOfShips = 0;
        if (player.destroyer2.Count != 0)
        {
            amountOfShips++;
        }
        if (player.crusier3.Count != 0)
        {
            amountOfShips++;
        }
        if (player.submarine3.Count != 0)
        {
            amountOfShips++;
        }
        if (player.battleship4.Count != 0)
        {
            amountOfShips++;
        }
        if (player.carrier5.Count != 0)
        {
            amountOfShips++;
        }
        if(amountOfShips == 5)
        {
            turn = Turn.PLAYER;
        }
    }
}
    public enum Turn
    {
        SETUP,
        PLAYER,
        AI
    }
