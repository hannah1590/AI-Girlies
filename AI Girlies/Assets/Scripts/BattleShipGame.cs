using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipGame : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] AIManager aiManager;
    [SerializeField] PlayerTurn playerTurn;

    public static GridTile[] playerGrid;
    public GridTile[] aiGrid;
    private Turn turn = Turn.SETUP;

    private void Awake()
    {
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
    }

    private void Setup()
    {
        
    }
}
    enum Turn
    {
        SETUP,
        PLAYER,
        AI
    }
