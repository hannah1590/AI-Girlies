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
    private Color oldColor;

    private Color defaultColor = new Color(70f / 255, 80f / 255, 110f / 255);
    private Color hoverColor = new Color(49f / 255, 54f / 255, 69f / 255);
    private Color shipColor = new Color(56f / 255, 93f / 255, 255f / 255);
    private Color shipHover = new Color(44f / 255, 73f / 255, 199f / 255);

    private Color hitColor = new Color(219f / 255, 11f / 255, 25f / 255, 255f / 255);
    private Color hitHover = new Color(194f / 255, 8f / 255, 20f / 255);
    private Color missColor = new Color(125f / 255, 125f / 255, 125f / 255, 255f / 255);
    private Color missHover = new Color(110f / 255, 101f / 255, 110f / 255, 255f / 255);

    public bool isHover;
    [SerializeField] private Status status = Status.EMPTY;

    private void Start()
    {
        this.gameObject.tag = "Tile";
        boxCollider.isTrigger = true;
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MousePosition")
        {
            isHover = true;
            if (currentColor == defaultColor)
            {
                oldColor = currentColor;
                SetColor(hoverColor);
                currentColor = hoverColor;
            }
            if (currentColor == shipColor)
            {
                oldColor = currentColor;
                SetColor(shipHover);
                currentColor = shipHover;
            }
            if(currentColor == hitColor)
            {
                oldColor = currentColor;
                SetColor(hitHover);
                currentColor = hitHover;
            }
            if (currentColor == missColor)
            {
                oldColor = currentColor;
                SetColor(missHover);
                currentColor = missHover;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHover = false;
        if (currentColor == hoverColor)
        {
            SetColor(oldColor);
            currentColor = oldColor;
        }
        if (currentColor == shipHover)
        {
            SetColor(oldColor);
            currentColor = oldColor;
        }
        if (currentColor == hitHover)
        {
            SetColor(oldColor);
            currentColor = oldColor;
        }
        if (currentColor == missHover)
        {
            SetColor(oldColor);
            currentColor = oldColor;
        }
    }

    public void ShipHoverSet()
    {
        if (currentColor == defaultColor)
        {
            oldColor = currentColor;
            SetColor(shipHover);
            currentColor = shipHover;
        }
        if (currentColor == shipColor)
        {
            oldColor = currentColor;
            SetColor(shipHover);
            currentColor = shipHover;
        }
    }

    public void ShipHoverReset()
    {
        if (currentColor == shipHover)
        {
            SetColor(oldColor);
            currentColor = oldColor;
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
        currentColor = shipColor;
        SetColor(shipColor);
    }

    //Resets the color of the tile to empty tile color. Used for reseting the game
    public void ResetColor() { spriteRenderer.color = defaultColor; }

    public void Fire(GridTile t)
    {
        if(t.getStatus() == Status.SHIP)
        {
            t.setStatus(Status.HIT);
            currentColor = hitColor;
            t.SetColor(hitColor);
        }
        else
        {
            t.setStatus(Status.MISS);
            currentColor = missColor;
            t.SetColor(missColor);
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