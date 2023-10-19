using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class PlayerBasicMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float speed = 4f;
    private float rotationSpeed = 1f;

    private Rigidbody2D playerRB;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.getPlayerMovable())
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            movement = new Vector2(horizontalInput, verticalInput).normalized;

            playerRB.velocity = movement * speed;
            //playerRB.MovePosition(playerRB.position + movement * speed * Time.deltaTime);

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
}
