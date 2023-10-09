using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public GameObject objectToThrow;
    private Vector2 throwDirection;
    public float throwForce = 8.0f;//throw force
    private Rigidbody2D rb;
    public GameObject user; // Player
    private bool hasThrown = false; // Track if the object has been thrown

    
    void Start()
    {
        
        // Disable the object initially (make it invisible)
        //objectToThrow.SetActive(false);

        // Access the Rigidbody2D of the object to throw
        rb = objectToThrow.GetComponent<Rigidbody2D>();

        // Check if the Rigidbody2D exists (make sure the objectToThrow has a Rigidbody2D component)
        if (rb == null)
        {
            Debug.LogError("The objectToThrow must have a Rigidbody2D component.");
        }

        throwDirection = new Vector2(45f, 45f); // throw direction:  tan(theta)=1;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for input to throw the object
        
        if (Input.GetKeyDown(KeyCode.J) && !hasThrown)
        {
            // Make the object visible
            //objectToThrow.SetActive(true);

            // Position the object at the user's location
            objectToThrow.transform.position = user.transform.position;

            // Throw the object
            Throw();

            // Set hasThrown to true to prevent further throws
            hasThrown = true;
        }
    }

    void Throw()
    {
        // Apply a force to the object in the specified direction
        rb.velocity = throwDirection.normalized * throwForce;

        // Destroy the objectToThrow after 5 seconds
        Destroy(objectToThrow, 5f);
    }
   
}