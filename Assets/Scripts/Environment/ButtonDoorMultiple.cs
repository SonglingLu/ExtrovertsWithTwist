using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMultiple : MonoBehaviour
{
    public static bool button1unlocked = false;
    public static bool button2unlocked = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Button1"))
            {
                GetComponent<Renderer>().material.color = Color.green;
                button1unlocked = true;
            }

            if (gameObject.CompareTag("Button2"))
            {
                GetComponent<Renderer>().material.color = Color.green;
                button2unlocked = true;
            }

            // Perform your action on the "Door" here
            Door door = FindObjectOfType<Door>(); // Find the "Door" object

            if (button1unlocked && button2unlocked && door != null)
            {
                door.Open(); // Call a method on the "Door" script to perform the action
            }
        }
    }
}
