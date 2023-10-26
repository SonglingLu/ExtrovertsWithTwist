using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    //public float floorDistance = 0.3f;

    private PlayerTransport playerTransport;

    private void Start()
    {
        playerTransport = FindObjectOfType<PlayerTransport>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //string tag = other.gameObject.tag;
        //string name = other.gameObject.name;
        //int layer = other.gameObject.layer;
        //Debug.Log(tag);
        //Debug.Log(name);
        //Debug.Log(layer);

        int playerLayer = LayerMask.NameToLayer("Player");
        //Debug.Log(playerLayer);


        // This method is called when a collision occurs with a non-trigger Collider
        if (other.gameObject.layer == playerLayer)
        {
            // You can perform actions here without blocking the player
            Debug.Log("Player entered the trigger zone.");
            playerTransport.MovePlayer(0, -30, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // This method is called when the player exits the trigger zone
        if (other.gameObject.CompareTag("Body"))
        {
            Debug.Log("Player exited the trigger zone.");
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Player exited the trigger zone.");
    //    }
    //}
}