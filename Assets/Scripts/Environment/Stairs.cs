using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour
{
    public string direction;

    private PlayerTransport playerTransport;

    private void Start()
    {
        playerTransport = FindObjectOfType<PlayerTransport>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            if (direction == "up")
            {
                // You can perform actions here without blocking the player
                FindObjectOfType<PlayerBasicMovement>().MovePlayer(0, 29, 0);
            }
            else {
                FindObjectOfType<PlayerBasicMovement>().MovePlayer(0, -31, 0);
            }

        }
    }

}