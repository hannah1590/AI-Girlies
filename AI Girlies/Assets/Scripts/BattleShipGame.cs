using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipGame : MonoBehaviour
{
    [SerializeField] GridManager gridManager;

    public GridTile[] playerGrid;
    public GridTile[] aiGrid;
    enum Turn
    {
        SETUP,
        PLAYER,
        AI
    }

    private void Awake()
    {
        playerGrid = gridManager.InitalizeGrid(Vector2.zero);
        aiGrid = gridManager.InitalizeGrid(new Vector2(0, 20));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {

    }
}
