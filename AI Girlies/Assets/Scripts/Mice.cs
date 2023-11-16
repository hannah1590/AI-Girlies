using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Mice : MonoBehaviour
{
    private Player player;
    private bool isTriggered = false;
    private string tileName;
    Vector3 mousePos;
    Vector3 worldPosition;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        gameObject.tag = "MousePosition";
        CircleCollider2D circle =  gameObject.AddComponent<CircleCollider2D>();//.isTrigger = true;
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
        circle.radius = 0.1f;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        gameObject.transform.position = worldPosition;

        if(isTriggered && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log(tileName);
            player.PlaceBoat(tileName);
            isTriggered = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {   
        if(collision.gameObject.tag == "Tile")
        {
            isTriggered = true;
            tileName = collision.gameObject.name;
        }    
    }
}
