using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTransport : MonoBehaviour
{
    //starting floor always 0
    //max and min are relative to starting floor
    private int currentFloor = 0;
    public int maxFloor;
    public int minFloor;

    public void MovePlayer(int xDirection, int yDirection, int zDirection)
    {
        //prevent accidentally trigger up or down multiple times
        if (yDirection > 0 && currentFloor == maxFloor || yDirection < 0 && currentFloor == minFloor)
        {
            return;
        }
        else {
            if (yDirection > 0)
            {
                currentFloor += 1;
            }
            else {
                currentFloor -= 1;
            }
            Vector3 newPosition = new Vector3(transform.position.x + xDirection, transform.position.y + yDirection, transform.position.z + zDirection);
            transform.position = newPosition;
        }

    }

}