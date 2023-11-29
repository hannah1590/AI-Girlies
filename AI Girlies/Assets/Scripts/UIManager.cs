using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI carrierText;
    [SerializeField] private TextMeshProUGUI battleshipText;
    [SerializeField] private TextMeshProUGUI crusierText;
    [SerializeField] private TextMeshProUGUI submarineText;
    [SerializeField] private TextMeshProUGUI destroyerText;

    [SerializeField] private TextMeshProUGUI carrierName;
    [SerializeField] private TextMeshProUGUI battleshipName;
    [SerializeField] private TextMeshProUGUI crusierName;
    [SerializeField] private TextMeshProUGUI submarineName;
    [SerializeField] private TextMeshProUGUI destroyerName;

    [SerializeField] private Player player;

    // Updates amount of spaces left in boat and turns name red when boat has sunk
    public void UpdateText(string boatName, int count)
    {
        if (boatName == "carrier")
        {
            carrierText.text = count.ToString();
            if(count == 0)
            {
                carrierText.color = Color.red;
                carrierName.color = Color.red;
            }
        }
        if (boatName == "battleship")
        {
            battleshipText.text = count.ToString();
            if (count == 0)
            {
                battleshipText.color = Color.red;
                battleshipName.color = Color.red;
            }
        }
        if (boatName == "crusier")
        {
            crusierText.text = count.ToString();
            if (count == 0)
            {
                crusierText.color = Color.red;
                crusierName.color = Color.red;
            }
        }
        if (boatName == "submarine")
        {
            submarineText.text = count.ToString();
            if (count == 0)
            {
                submarineText.color = Color.red;
                submarineName.color = Color.red;
            }
        }
        if (boatName == "destroyer")
        {
            destroyerText.text = count.ToString();
            if (count == 0)
            {
                destroyerText.color = Color.red;
                destroyerName.color = Color.red;
            }
        }
    }

    // sets which ship is currently being placed
    public void ButtonCar()
    {
        if (player.carrier5.Count == 0)
        {
            player.shipIndex = 5;
            player.currentBoat = "carrier";
            player.isPlacing = true;
        }
    }

    public void ButtonBattle()
    {
        if (player.battleship4.Count == 0)
        {
            player.shipIndex = 4;
            player.currentBoat = "battleship";
            player.isPlacing = true;
        }
    }

    public void ButtonCrusier()
    {
        if (player.crusier3.Count == 0)
        {
            player.shipIndex = 3;
            player.currentBoat = "crusier";
            player.isPlacing = true;
        }
    }

    public void ButtonSub()
    {
        if (player.submarine3.Count == 0)
        {
            player.shipIndex = 3;
            player.currentBoat = "submarine";
            player.isPlacing = true;
        }
    }

    public void ButtonDest()
    {
        if (player.destroyer2.Count == 0)
        {
            player.shipIndex = 2;
            player.currentBoat = "destroyer";
            player.isPlacing = true;
        }
    }
}
