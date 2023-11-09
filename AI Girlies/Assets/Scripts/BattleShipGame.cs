using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipGame : MonoBehaviour
{
    [SerializeField] GridManager gridManager;
    [SerializeField] AIManager aiManager;

    public static GridTile[] playerGrid;
    public GridTile[] aiGrid;
    private Turn turn = Turn.SETUP;

    private void Awake()
    {
        playerGrid = gridManager.InitalizeGrid(Vector2.zero);
        aiGrid = gridManager.InitalizeGrid(new Vector2(0, 20));

        //aiManager.MakeBoard();
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
