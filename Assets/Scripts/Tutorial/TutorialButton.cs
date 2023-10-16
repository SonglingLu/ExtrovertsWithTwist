using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    private bool doorCheck = false;
    private void Start()
    {

        // Your other initialization code here...
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        // Check if the collision involves the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;

            // Perform your action on the "Door" here
            Door door = FindObjectOfType<Door>(); // Find the "Door" object

            if (door != null)
            {
                door.Open();
                if (!doorCheck)
                {
                    TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
                    if (myScriptInstance != null)
                    {
                        myScriptInstance.LoadNextStep();
                    }
                    doorCheck = true;
                }
                
            }
        }
    }
}
