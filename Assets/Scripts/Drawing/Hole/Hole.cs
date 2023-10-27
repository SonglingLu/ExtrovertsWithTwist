using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    //public float floorDistance = 0.3f;

   


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // This method is called when a collision occurs with a non-trigger Collider
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Body"))
        {
            
           FindObjectOfType<PlayerBasicMovement>().MovePlayer(0, -30, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // This method is called when the player exits the trigger zone
        if (other.gameObject.CompareTag("Body"))
        {
           
        }
    }




}