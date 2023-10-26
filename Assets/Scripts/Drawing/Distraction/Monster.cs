using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 1.0f;//monster's speed
    public GameObject objectToThrow; // distraction item
    private bool isMoving = false; // monster is not moving initially
    
    private Vector3 originalPosition;//monstor's original position

    private float positionThreshold = 0.2f;//positionthreshold

    void Start()
    {
        // Store the original position of Monster
        originalPosition = transform.position;
    }
    void Update()
    {
        if (objectToThrow != null)//if the distraction item exists
        {
            // Calculate the direction from this object to objectToThrow.
            Vector3 directionToMonster = objectToThrow.transform.position - transform.position;

            // Calculate the distance to objectToThrow.
            float distanceToMonster = directionToMonster.magnitude;

            // Check if objectToThrow is within a certain range 
            if (distanceToMonster < 5f && distanceToMonster > 0.85f)
            {
                // Start moving when distance is between 0.85 and 5 units.
                isMoving = true;
            }
            else
            {
                // Stop moving when distance is less than 0.85 or greater than 5 units. 
                //i.e too far away or close enough between the monster and distraction item
                isMoving = false;
            }

            if (isMoving)
            {
                // Be attracted by the distraction item. Move towards objectToThrow slowly.
                transform.position += directionToMonster.normalized * moveSpeed * Time.deltaTime;
            }

        }else{

            // Calculate the direction from current position to original position
            Vector3 directionToOriginal = originalPosition - transform.position;

            // Move back to the original position.
            if(directionToOriginal.magnitude>positionThreshold){

                transform.position += directionToOriginal.normalized * moveSpeed * Time.deltaTime;

            }
            

        }
    }
}