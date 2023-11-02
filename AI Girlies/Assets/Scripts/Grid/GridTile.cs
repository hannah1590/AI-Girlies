using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTile : MonoBehaviour
{
    public Vector2Int gridCords;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor = new Color(79, 87, 110, 255);
    private Color shipColor = new Color(56, 93, 255, 255);
    private Status status = Status.EMPTY;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    public void SetColor(Color color) { spriteRenderer.color = color; }
    public Status getStatus() {  return status; }
    public void setStatus(Status s) { status = s; }

    public void SetShip()
    {
        status = Status.SHIP;
        SetColor(shipColor);
    }

    //Resets the color of the tile to empty tile color. Used for reseting the game
    public void ResetColor() { spriteRenderer.color = defaultColor; }

    public void Fire(GridTile t)
    {
        if(t.getStatus() == Status.SHIP)
        {
            t.setStatus(Status.HIT);
            t.SetColor(new Color(219, 11, 25, 255));
        }
        else
        {
            t.setStatus(Status.MISS);
            t.SetColor(new Color(125, 125, 125, 255));
        }
    }
}

public enum Status
{
    SHIP,
    EMPTY,
    HIT,
    MISS
}