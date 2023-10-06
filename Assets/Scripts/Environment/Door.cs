using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isUnlocked = false;
    SpriteRenderer sprite;
    private void Start()
    {

    }

    // Method to open or close the door
    public void Open()
    {
        Debug.Log("open called");
        GetComponent<SpriteRenderer>().color = Color.green;
        isUnlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player") && isUnlocked)
        {
            Debug.Log("you win");
        }
    }
}