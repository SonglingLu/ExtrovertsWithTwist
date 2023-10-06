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
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetToMouse;
            mousePosition.z = transform.position.z; 
            Vector3 direction = mousePosition - transform.parent.parent.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            transform.parent.parent.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 2000 * Time.deltaTime);
        }
    }

    void OnMouseUp()
    {
        isMouseDragging = false;
    }
}
