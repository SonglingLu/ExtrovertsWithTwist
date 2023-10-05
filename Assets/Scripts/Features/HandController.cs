using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private bool isMouseDragging = false;
    private Vector3 offsetToMouse;

    void OnMouseDown()
    {
        // Calculate the offset between the tipOfHand and the mouse position
        offsetToMouse = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isMouseDragging = true;
    }

    void OnMouseDrag()
    {
        if (isMouseDragging)
        {
            // Get the current mouse position in world coordinates
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetToMouse;
            newPosition.z = 0; // Make sure the z-coordinate is 0 for 2D.

            // Move the tipOfHand to the new position
            transform.position = newPosition;

            transform.rotation = Quaternion.identity;
        }
    }

    void OnMouseUp()
    {
        isMouseDragging = false;
    }
}
