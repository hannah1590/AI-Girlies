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
}
