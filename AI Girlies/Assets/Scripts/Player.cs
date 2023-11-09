using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int[] ships = { 2, 3, 3, 4, 5 };
    private int index = 0;
    private float mouseScroll = 0.2f;

    public void Update()
    {
        
        if(Input.mouseScrollDelta.y != 0f)
        {
            if (index < 5 && Input.mouseScrollDelta.y > 0.0f)
                index++;
            if (index > 0 && Input.mouseScrollDelta.y < 0.0f)
                index--;
        }
    }

    private void OnMouseOver()
    {
        
    }

    public void Place()
    {
        //BattleShipGame.playerGrid

        //scroll to change between the ships that are left
        //right click to rotate
        //left click to place
    }



}