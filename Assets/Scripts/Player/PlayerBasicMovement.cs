using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class PlayerBasicMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 4f;
    private float rotationSpeed = 2.5f;

    private Rigidbody2D playerRB;
    private Vector2 movement;

    public bool RotateInDirection = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.GetPlayerMovable())
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            movement = new Vector2(horizontalInput, verticalInput).normalized;
            if(movement.magnitude>0)
            {
                GetComponent<Animator>().SetBool("isWalking", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("isWalking", false);
            }
            playerRB.velocity = movement * speed;
            //playerRB.MovePosition(playerRB.position + movement * speed * Time.deltaTime);

            if(RotateInDirection)
            {
                float angle = Mathf.Atan2(-movement.x, movement.y) * Mathf.Rad2Deg;

                if (movement.magnitude != 0)
                {

                    float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, Time.deltaTime * 5);
                    playerRB.MoveRotation(currentAngle);
                }
            }
           

            if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            {
                playerRB.MoveRotation(playerRB.rotation - rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E))
            {
                playerRB.MoveRotation(playerRB.rotation + rotationSpeed);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Step2Collider")
        {
            TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
            if (myScriptInstance != null)
            {
                myScriptInstance.LoadNextStep();
            }
        }
    }






    public int currentFloor = 0;
    public int maxFloor;
    public int minFloor;

    public void MovePlayer(int xDirection, int yDirection, int zDirection)
    {
        Camera.main.GetComponent<CameraFade>().RedoFade();
        //prevent accidentally trigger up or down multiple times
        if (yDirection > 0 && currentFloor == maxFloor || yDirection < 0 && currentFloor == minFloor)
        {
            return;
        }
        else
        {
            if (yDirection > 0)
            {
                currentFloor += 1;
            }
            else
            {
                currentFloor -= 1;
            }
            Vector3 newPosition = new Vector3(transform.position.x + xDirection, transform.position.y + yDirection, transform.position.z + zDirection);
            transform.position = newPosition;
        }

    }


}
