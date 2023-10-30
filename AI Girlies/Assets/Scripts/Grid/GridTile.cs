using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector2Int gridCords;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    //sets the color of the tile to a certain color
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    //Resets the color of the tile to default color
    public void ResetColor()
    {
        spriteRenderer.color = defaultColor;
    }
}
