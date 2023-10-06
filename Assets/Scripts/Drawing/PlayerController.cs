using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the movement speed

    private Rigidbody2D rb;

    private void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get input for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        // Apply movement to the Rigidbody2D
        rb.velocity = movement * moveSpeed;
    }
}

