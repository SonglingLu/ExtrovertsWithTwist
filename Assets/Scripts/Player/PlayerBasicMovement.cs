using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerBasicMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 5f;
    private float rotationSpeed = 6f;
    private float rotationSpeedEQ = 100f;
    public bool RotateInDirection = false;

    Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        transform.Translate(movementDirection * Time.deltaTime * speed * inputMagnitude, Space.World);

       


        if(RotateInDirection)
        {
            // Rotate the player to face the movement direction
            if (movementDirection != Vector2.zero)
            {
                //float angle = Mathf.Atan2(movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
               // transform.rotation = Quaternion.AngleAxis(angle, Vector3.back);
                float targetAngle = Mathf.Atan2(-movementDirection.x, movementDirection.y) * Mathf.Rad2Deg;
                float angle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, targetAngle, rotationSpeedEQ * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(Vector3.back, Time.deltaTime * rotationSpeedEQ);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(Vector3.forward, Time.deltaTime * rotationSpeedEQ);
            }
        }
        
    }
}
