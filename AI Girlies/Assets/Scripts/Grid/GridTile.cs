using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridTile : MonoBehaviour
{
    public Vector2Int gridCords;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private Color currentColor;

    private Color defaultColor = new Color(70f / 255, 80f / 255, 110f / 255);
    private Color hoverColor = new Color(49f / 255, 54f / 255, 69f / 255);
    private Color shipColor = new Color(56f / 255, 93f / 255, 255f / 255);
    private Color shipHover = new Color(44f / 255, 73f / 255, 199f / 255);

    private Status status = Status.EMPTY;

    private void OnMouseOver()
    {
        if (currentColor == defaultColor) {
            SetColor(hoverColor);
            currentColor = hoverColor;
        }
        if (currentColor == shipColor) { 
            SetColor(shipHover);
            currentColor = shipHover;
        }
    }

    private void OnMouseExit()
    {
        if (currentColor == hoverColor) {
            SetColor(defaultColor);
            currentColor = defaultColor; 
        }
        if (currentColor == shipHover) {
            SetColor(shipColor); 
            currentColor = shipColor; 
        }
    }

    private void Awake()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentColor = defaultColor;
        SetColor(currentColor);
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