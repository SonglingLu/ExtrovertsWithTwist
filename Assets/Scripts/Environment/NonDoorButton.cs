using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonDoorButton : MonoBehaviour
{
    public static bool button1unlocked = false;
    public static bool button2unlocked = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;

            // Perform your action on the "Door" here
            Obstacle obstacle = FindObjectOfType<Obstacle>(); // Find the "Door" object

            if (obstacle != null)
            {
                Debug.Log("obstacle remove called");
                obstacle.Open(); // Call a method on the "Door" script to perform the action
            }
        }
    }
}
