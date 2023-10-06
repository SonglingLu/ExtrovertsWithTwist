using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private void Start()
    {

        // Your other initialization code here...
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Renderer>().material.color = Color.green;

            // Perform your action on the "Door" here
            Door door = FindObjectOfType<Door>(); // Find the "Door" object

            if (door != null)
            {
                door.Open(); // Call a method on the "Door" script to perform the action
            }
        }
    }
}
